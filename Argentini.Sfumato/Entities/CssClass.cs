// ReSharper disable PropertyCanBeMadeInitOnly.Global

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities;

public sealed class CssClass
{
    public CssClass(AppState appState, string name)
    {
        AppState = appState;
        Name = name;
    }
    
    public AppState? AppState { get; set; }
    
    /// <summary>
    /// Utility class name from scanned files.
    /// (e.g. "dark:tabp:text-base/6")
    /// </summary>
    public string Name
    {
        get => _name;
        
        set
        {
            _name = value;

            CssSelector = string.Empty;

            AllSegments.Clear();
            VariantSegments.Clear();
            CoreSegments.Clear();

            IsImportant = _name.EndsWith('!');
            
            AllSegments.AddRange(ContentScanner.SplitByColonsRegex().Split(_name.TrimEnd('!')));

            EscapeCssClassName();
            ProcessData();
        }
    }
    private string _name = string.Empty;

    /// <summary>
    /// Name broken into variant and core segments.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]", "text-base/6"])
    /// </summary>
    public List<string> AllSegments { get; } = [];

    /// <summary>
    /// Variant segments used in the class name.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]"])
    /// </summary>
    public List<string> VariantSegments { get; } = [];

    /// <summary>
    /// Core class segments; only one segment for static utilities.
    /// (e.g. "dark:tabp:text-base/6" => ["text-", "base", "6"])
    /// (e.g. "dark:tabp:-min-w-10" => ["-min-w-", "10"])
    /// (e.g. "antialiased" => ["antialiased"])
    /// </summary>
    public List<string> CoreSegments { get; } = [];

    /// <summary>
    /// CSS selector generated from the Name; 
    /// (e.g. "dark:tabp:text-base/6" => ".dark\:tabp\:text-base\/6")
    /// </summary>
    public string CssSelector { get; set; } = string.Empty;

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition;

    #region Properties

    public bool IsValid { get; set; }
    public bool IsCustomCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    public bool IsImportant { get; set; }
    
    #endregion
    
    #region Initialization

    private void ProcessData()
    {
        if (AppState is null || string.IsNullOrEmpty(Name))
            return;

        #region Variants
        
        if (AllSegments.Count > 1)
            VariantSegments.AddRange(AllSegments.Take(AllSegments.Count - 1));

        // todo: validate variants
        
        #endregion
        
        #region Custom CSS
        
        if (AllSegments[^1][0] == '[' && AllSegments[^1][^1] == ']')
        {
            if (ContentScanner.PatternCssCustomPropertyAssignmentRegex().Match(AllSegments[^1].TrimStart('[').TrimEnd(']')).Success)
            {
                // [--my-color-var:red]
                CoreSegments.Add(AllSegments[^1]);
                IsCssCustomPropertyAssignment = true;
                IsValid = true;
                
                return;
            }

            if (AppState.Library.CssPropertyNamesWithColons.Any(substring => AllSegments[^1].Contains(substring, StringComparison.Ordinal)))
            {
                // [color:red]
                CoreSegments.Add(AllSegments[^1]);
                IsCustomCss = true;
                IsValid = true;
                
                return;
            }

            return;
        }

        #endregion

        #region Utility Classes
        
        var prefix = string.Empty;
        
        foreach (var utility in AppState.Library.ScannerClassNamePrefixes.OrderByDescending(p => p.Length))
        {
            if (AllSegments[^1].StartsWith(utility, StringComparison.Ordinal) == false)
                continue;
            
            prefix = utility;

            break;
        }

        if (string.IsNullOrEmpty(prefix))
            return;
        
        var value = AllSegments[^1].TrimStart(prefix) ?? string.Empty;
        var modifier = string.Empty;
        var slashSegments = ContentScanner.SplitBySlashesRegex().Split(value);

        if (slashSegments.Length == 2)
        {
            modifier = slashSegments[^1];
            value = value.TrimEnd($"/{modifier}") ?? string.Empty;
        }

        CoreSegments.Add(prefix);

        if (string.IsNullOrEmpty(value) == false)
            CoreSegments.Add(value);

        if (string.IsNullOrEmpty(modifier) == false)
            CoreSegments.Add(modifier);

        IsValid = true;

        if (value.StartsWith('[') && value.EndsWith(']'))
        {
            var trimmedValue = value.TrimStart('[').TrimEnd(']');
            
            if (ValueIsLength(trimmedValue))
                AppState.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition);
            else if (ValueIsColorName(trimmedValue) || trimmedValue.IsValidWebColor())
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
            else if (ValueIsAngle(trimmedValue))
                AppState.Library.AngleClasses.TryGetValue(prefix, out ClassDefinition);
            else if (ValueIsFrequency(trimmedValue))
                AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
            else if (ValueIsResolution(trimmedValue))
                AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
        }
        else
        {
            if (string.IsNullOrEmpty(value))
            {
                AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);
            }
            else if (ValueIsNumber(value))
            {
                if (AppState.Library.NumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                {
                    if (AppState.Library.AngleClasses.TryGetValue(prefix, out ClassDefinition) == false)
                    {
                        if (AppState.Library.DurationClasses.TryGetValue(prefix, out ClassDefinition) == false)
                        {
                            if (AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition) == false)
                            {
                                AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                            }
                        }
                    }
                }
            }
            else if (ValueIsLength(value))
            {
                AppState.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition);
            }
            else if (ValueIsColorName(value))
            {
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
            }
        }

        #endregion
    }

    #endregion
    
    #region Helpers
    
    /// <summary>
    /// Escape the CSS class name to be used in a CSS selector.
    /// </summary>
    /// <returns></returns>
    private void EscapeCssClassName()
    {
        CssSelector = string.Empty;
        
        if (AppState is null || string.IsNullOrEmpty(Name))
            return;

        var value = AppState.StringBuilderPool.Get();

        try
        {
            for (var i = 0; i < Name.Length; i++)
            {
                var c = Name[i];

                if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                    value.Append('\\');

                value.Append(c);
            }

            /*
            var peerGroupSegment = IsPeerGroupSelector(Name, "peer") ? "peer" : IsPeerGroupSelector(Name, "group") ? "group" : string.Empty;

            if (string.IsNullOrEmpty(peerGroupSegment))
                CssSelector = value.ToString();
            
            if (string.IsNullOrEmpty(PeerGroupPrefix))
            {
                IsInvalid = HasPeerGroupVariant(appState, Name, peerGroupSegment, out _, out var peerGroupPrefix) == false;

                if (IsInvalid)
                    CssSelector = value.ToString();
		            
                PeerGroupPrefix = peerGroupPrefix;
            }

            value.Insert(
                0,
                peerGroupSegment == "peer" ? $"{PeerGroupPrefix}~." : $"{PeerGroupPrefix} .");
                */

            CssSelector = value.ToString();
        }
        finally
        {
            AppState?.StringBuilderPool.Return(value);
        }
    }

    private bool ValueIsNumber(string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.');
    }

    private bool ValueIsLength(string value)
    {
        var unit = AppState?.Library.CssUnits.FirstOrDefault(unit => value.EndsWith(unit, StringComparison.Ordinal)) ?? string.Empty;

        return string.IsNullOrEmpty(unit) == false && ValueIsNumber(value.TrimEnd(unit) ?? string.Empty);
    }

    private bool ValueIsAngle(string value)
    {
        var unit = AppState?.Library.CssAngleUnits.FirstOrDefault(unit => value.EndsWith(unit, StringComparison.Ordinal)) ?? string.Empty;

        return string.IsNullOrEmpty(unit) == false && ValueIsNumber(value.TrimEnd(unit) ?? string.Empty);
    }

    private bool ValueIsFrequency(string value)
    {
        var unit = AppState?.Library.CssFrequencyUnits.FirstOrDefault(unit => value.EndsWith(unit, StringComparison.Ordinal)) ?? string.Empty;

        return string.IsNullOrEmpty(unit) == false && ValueIsNumber(value.TrimEnd(unit) ?? string.Empty);
    }

    private bool ValueIsResolution(string value)
    {
        var unit = AppState?.Library.CssResolutionUnits.FirstOrDefault(unit => value.EndsWith(unit, StringComparison.Ordinal)) ?? string.Empty;

        return string.IsNullOrEmpty(unit) == false && ValueIsNumber(value.TrimEnd(unit) ?? string.Empty);
    }

    private bool ValueIsColorName(string value)
    {
        return AppState?.Library.Colors.ContainsKey(value) ?? false;
    }

    #endregion
}