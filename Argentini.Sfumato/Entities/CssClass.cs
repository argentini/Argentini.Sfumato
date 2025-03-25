// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

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
            IsValid = false;

            AllSegments.Clear();
            VariantSegments.Clear();
            CoreSegments.Clear();
            
            AllSegments.AddRange(ContentScanner.SplitByColonsRegex().Split(_name.TrimEnd('!')));

            ProcessData();

            if (IsValid == false)
                return;

            IsImportant = _name.EndsWith('!');

            EscapeCssClassName();
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
        {
            // One or more invalid variants invalidate the entire utility class
            
            foreach (var segment in AllSegments.Take(AllSegments.Count - 1))
            {
                var variant = segment.TrimStart("max-").TrimStart("not-") ?? string.Empty;

                if (string.IsNullOrEmpty(variant))
                    return;

                if (AppState.Library.MediaQueryPrefixes.ContainsKey(variant))
                {
                    // tabp: dark: etc.
                    
                    VariantSegments.Add(segment);
                }
                else if (AppState.Library.PseudoclassPrefixes.ContainsKey(variant))
                {
                    // hover: focus: etc.
                    
                    VariantSegments.Add(segment);
                }
                else if (segment.StartsWith("group-"))
                {
                    if (segment.StartsWith("group-has-"))
                    {
                        // group-has-[a]:
                        
                        VariantSegments.Add(segment);
                    }
                    else if (segment.StartsWith("group-aria-"))
                    {
                        // group-aria-checked:
                        
                        VariantSegments.Add(segment);
                    }
                    else
                    {
                        // group-hover: group-focus: etc.
                        
                        variant = segment.TrimStart("group-") ?? string.Empty;
                
                        if (string.IsNullOrEmpty(variant))
                            return;
                
                        var indexOfSlash = variant.LastIndexOf('/');

                        if (indexOfSlash > 0)
                            variant = variant[..indexOfSlash];

                        if (variant.StartsWith('[') && variant.EndsWith(']'))
                        {
                            VariantSegments.Add(segment);
                        }
                        else if (AppState.Library.PseudoclassPrefixes.ContainsKey(variant))
                        {
                            VariantSegments.Add(segment);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (segment.StartsWith("peer-"))
                {
                    if (segment.StartsWith("peer-has-"))
                    {
                        // peer-has-[a]:

                        VariantSegments.Add(segment);
                    }
                    else if (segment.StartsWith("peer-aria-"))
                    {
                        // peer-aria-checked:

                        VariantSegments.Add(segment);
                    }
                    else
                    {
                        // peer-hover: peer-focus: etc.

                        variant = segment.TrimStart("peer-") ?? string.Empty;
                
                        if (string.IsNullOrEmpty(variant))
                            return;
                
                        var indexOfSlash = variant.LastIndexOf('/');

                        if (indexOfSlash > 0)
                            variant = variant[..indexOfSlash];

                        if (variant.StartsWith('[') && variant.EndsWith(']'))
                        {
                            VariantSegments.Add(segment);
                        }
                        else if (AppState.Library.PseudoclassPrefixes.ContainsKey(variant))
                        {
                            VariantSegments.Add(segment);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (segment.StartsWith("in-"))
                {
                    // in-hover: in-focus: etc.

                    VariantSegments.Add(segment);
                }
                else if (segment.StartsWith("nth-"))
                {
                    if (segment.StartsWith("nth-of-type-"))
                    {
                        VariantSegments.Add(segment);
                    }
                    else if (segment.StartsWith("nth-last-of-type-"))
                    {
                        VariantSegments.Add(segment);
                    }
                    else if (segment.StartsWith("nth-last-"))
                    {
                        VariantSegments.Add(segment);
                    }
                    else
                    {
                        VariantSegments.Add(segment);
                    }
                }
                else if (segment.StartsWith("has-"))
                {
                    // has-hover: has-focus: etc.

                    VariantSegments.Add(segment);
                }
                else if (segment.StartsWith("supports-"))
                {
                    // supports-[display:grid]:
                    
                    VariantSegments.Add(segment);
                }
                else if (segment.StartsWith("not-supports-"))
                {
                    // not-supports-[display:grid]:

                    VariantSegments.Add(segment);
                }
                else if (segment.StartsWith("data-"))
                {
                    // data-active: data-myattribute: data-[size=large] etc.
                    
                    VariantSegments.Add(segment);
                }
                else if ((segment.StartsWith("[&") || segment.StartsWith("[@")) && segment.EndsWith(']'))
                {
                    // [&.class] or [&_p] or [@supports(display:grid)] etc.
                    
                    VariantSegments.Add(segment);
                }
                else
                {
                    return;
                }
            }
        }
        
        #endregion
        
        #region Custom CSS
        
        if (AllSegments[^1][0] == '[' && AllSegments[^1][^1] == ']')
        {
            var trimmedValue = AllSegments[^1].TrimStart('[').TrimEnd(']').Trim('_');
            var colonIndex = trimmedValue.IndexOf(':');
            
            if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                return;
            
            if (trimmedValue.StartsWith("--", StringComparison.Ordinal))
            {
                // [--my-color-var:red]
                CoreSegments.Add(trimmedValue);
                IsCssCustomPropertyAssignment = true;
                IsValid = true;
                
                return;
            }
            
            /*
            if (ContentScanner.PatternCssCustomPropertyAssignmentRegex().Match(trimmedValue).Success)
            {
                // [--my-color-var:red]
                CoreSegments.Add(trimmedValue);
                IsCssCustomPropertyAssignment = true;
                IsValid = true;
                
                return;
            }tr
            */

            if (AppState.Library.CssPropertyNamesWithColons.HasPrefixIn(trimmedValue) == false)
                return;

            // [color:red]
            CoreSegments.Add(trimmedValue);
            IsCustomCss = true;
            IsValid = true;
                
            return;
        }

        #endregion

        #region Utility Classes
        
        var prefix = AppState.Library.ScannerClassNamePrefixes.GetLongestMatchingPrefix(AllSegments[^1]);
        
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

        if ((value.StartsWith('[') && value.EndsWith(']')) || (value.StartsWith('(') && value.EndsWith(')')))
        {
            var customValue = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

            // Specified arbitrary value data type prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")
            if (customValue.StartsWith("alpha:", StringComparison.Ordinal) || customValue.StartsWith("number:", StringComparison.Ordinal))
                AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("angle:", StringComparison.Ordinal) || customValue.StartsWith("hue:", StringComparison.Ordinal))
                AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("color:", StringComparison.Ordinal))
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("dimension:", StringComparison.Ordinal) || customValue.StartsWith("length:", StringComparison.Ordinal))
                AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("duration:", StringComparison.Ordinal) || customValue.StartsWith("time:", StringComparison.Ordinal))
                AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("flex:", StringComparison.Ordinal))
                AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("frequency:", StringComparison.Ordinal))
                AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("image:", StringComparison.Ordinal) || customValue.StartsWith("url:", StringComparison.Ordinal))
                AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("integer:", StringComparison.Ordinal))
                AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("percentage:", StringComparison.Ordinal))
                AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("ratio:", StringComparison.Ordinal))
                AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("resolution:", StringComparison.Ordinal))
                AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
            else if (customValue.StartsWith("string:", StringComparison.Ordinal))
                AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
            else
            {
                if (value.StartsWith("(--", StringComparison.Ordinal) || value.StartsWith("[var(--", StringComparison.Ordinal))
                {
                    AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
                    
                    if (ClassDefinition is null)
                        AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                    
                    if (ClassDefinition is null)
                        AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                    
                    if (ClassDefinition is null)
                        AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                    
                    if (ClassDefinition is null)
                        AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                    
                    if (ClassDefinition is null)
                        AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else
                {
                    // Auto-detect value type

                    if (ValueIsAlphaNumber(customValue))
                    {
                        if (AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                            AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                    }
                    else if (ValueIsInteger(customValue))
                        AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsColorName(customValue) || customValue.IsValidWebColor())
                        AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsPercentage(customValue))
                        AppState.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsAngleHue(customValue))
                        AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsDimensionLength(customValue))
                        AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsDurationTime(customValue))
                        AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsFrequency(customValue))
                        AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsImageUrl(customValue))
                        AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsRatio(customValue))
                        AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (ValueIsResolution(customValue))
                        AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                    else
                    {
                        if (AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition) == false)
                            AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                    }
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(value))
                AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);

            else if (ValueIsInteger(value))
                AppState.Library.SpacingClasses.TryGetValue(prefix, out ClassDefinition);

            else if (ValueIsColorName(value))
                AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
        }

        if (ClassDefinition is not null)
            IsValid = true;

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

    private string GetUnit(string value)
    {
        var index = 0;

        while (index < value.Length && (char.IsDigit(value[index]) || value[index] == '.'))
            index++;

        return index >= value.Length ? string.Empty : value[index..];         
    }
    
    private bool ValueIsAlphaNumber(string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.');
    }

    private bool ValueIsAngleHue(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssAngleUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsColorName(string value)
    {
        return AppState?.Library.Colors.ContainsKey(value) ?? false;
    }

    private bool ValueIsDimensionLength(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssLengthUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsDurationTime(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssDurationUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsFrequency(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssFrequencyUnits.Any(u => u == unit) ?? false;
    }

    private bool ValueIsImageUrl(string value)
    {
        return value.StartsWith("url(", StringComparison.Ordinal) || Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);
    }

    private bool ValueIsInteger(string value)
    {
        return value.All(char.IsDigit);
    }

    private bool ValueIsRatio(string value)
    {
        var segments = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length != 2)
            return false;
        
        return int.TryParse(segments[0].Trim(), out _) && int.TryParse(segments[1].Trim(), out _);
    }

    private bool ValueIsPercentage(string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.') && value.EndsWith('%');
    }

    private bool ValueIsResolution(string value)
    {
        var unit = GetUnit(value);

        if (string.IsNullOrEmpty(unit))
            return false;
        
        return AppState?.Library.CssResolutionUnits.Any(u => u == unit) ?? false;
    }

    #endregion
}