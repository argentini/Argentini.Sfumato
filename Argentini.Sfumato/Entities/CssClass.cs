// ReSharper disable PropertyCanBeMadeInitOnly.Global

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities;

public sealed class CssClass
{
    public CssClass(AppState appState, string name)
    {
        Name = name;
        AppState = appState;

        EscapeCssClassName();
        ProcessData();
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

            AllSegments = new List<string>(ContentScanner.SplitByColonsRegex().Split(_name.TrimEnd('!'))).ToList();

            EscapeCssClassName();
            ProcessData();
        }
    }
    private string _name = string.Empty;

    /// <summary>
    /// Name broken into variant and core segments.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]", "text-base/6"])
    /// </summary>
    public List<string> AllSegments { get; set; } = [];

    /// <summary>
    /// Variant segments used in the class name.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]"])
    /// </summary>
    public List<string> VariantSegments { get; set; } = [];

    /// <summary>
    /// Core class segments; only one segment for static utilities.
    /// (e.g. "dark:tabp:text-base/6" => ["text-", "base", "6"])
    /// (e.g. "dark:tabp:-min-w-10" => ["-min-w-", "10"])
    /// (e.g. "antialiased" => ["antialiased"])
    /// </summary>
    public List<string> CoreSegments { get; set; } = [];

    /// <summary>
    /// CSS selector generated from the Name; 
    /// (e.g. "dark:tabp:text-base/6" => ".dark\:tabp\:text-base\/6")
    /// </summary>
    public string CssSelector { get; set; } = string.Empty;

    /// <summary>
    /// Determines the order of generated CSS classes;
    /// defaults to zero.
    /// </summary>
    public int SelectorSort { get; set; } = 0;

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition { get; set; }

    #region Properties

    public bool IsValid { get; set; }
    public bool IsCustomCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    
    #endregion
    
    #region Initialization

    private void ProcessData()
    {
        if (AppState is null || string.IsNullOrEmpty(Name))
            return;

        AllSegments.Clear();
        AllSegments.AddRange(ContentScanner.SplitByColonsRegex().Split(Name.TrimEnd('!')));

        VariantSegments.Clear();
        
        if (AllSegments.Count > 1)
            VariantSegments.AddRange(AllSegments.Take(AllSegments.Count - 1));

        CoreSegments.Clear();

        if (ClassDefinition is not null && ClassDefinition.IsSimpleUtility)
        {
            CoreSegments.Add(AllSegments[^1]);
        }
        else
        {
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
        }        
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
    
    #endregion
}