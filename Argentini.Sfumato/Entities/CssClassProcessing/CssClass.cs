// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.UtilityClasses;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.CssClassProcessing;

public sealed partial class CssClass : IDisposable
{
    #region Regular Expressions
    
    private const string PatternCssCustomPropertyAssignment = @"^--[\w-]+_?:_?[^;]+;?$";
    
    private const string SplitByColons = @":(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";

    private const string SplitBySlashes = @"/(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";
    
    [GeneratedRegex(PatternCssCustomPropertyAssignment, RegexOptions.Compiled)]
    public static partial Regex PatternCssCustomPropertyAssignmentRegex();

    [GeneratedRegex(SplitByColons, RegexOptions.Compiled)]
    public static partial Regex SplitByColonsRegex();

    [GeneratedRegex(SplitBySlashes, RegexOptions.Compiled)]
    public static partial Regex SplitBySlashesRegex();
    
    #endregion
    
    #region Properties
    
    public AppState AppState { get; set; }
    
    /// <summary>
    /// Utility class name from scanned files.
    /// (e.g. "dark:tabp:text-base/6")
    /// </summary>
    public string Selector { get; set; }

    /// <summary>
    /// Name broken into variant and core segments.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]", "text-base/6"])
    /// </summary>
    public List<string> AllSegments { get; } = [];

    /// <summary>
    /// Variant segments used in the class name.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]"])
    /// </summary>
    public Dictionary<string,VariantMetadata> VariantSegments { get; } = new(StringComparer.Ordinal);

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition;

    public List<string> Wrappers { get; } = [];

    public string EscapedSelector { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ModifierValue { get; set; } = string.Empty;
    public string Styles { get; set; } = string.Empty;
    public long SelectorSort { get; set; }

    public bool IsValid { get; set; }
    public bool IsArbitraryCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    public bool IsImportant { get; set; }
    public bool HasModifierValue { get; set; }
    public bool HasArbitraryModifierValue { get; set; }
    public bool HasArbitraryValue { get; set; }
    public bool HasArbitraryValueWithCssCustomProperty { get; set; }

    private StringBuilder? Sb { get; set; }

    #endregion
    
    #region Lifecycle

    public CssClass(AppState appState, string selector)
    {
        AppState = appState;
        Selector = selector;

        Initialize();
    }

    public void Dispose()
    {
        if (Sb is not null)
            AppState.StringBuilderPool.Return(Sb);
    }

    #endregion

    // todo: add full-spectrum utility class processing test ("text-..." may be a good one to use)
    // todo: create code to iterate, group, wrap classes, generate actual CSS (perhaps create a list of segments based on like media queries, then stack them in the final CSS)
    // todo: remove unused properties across all entities

    #region Initialization
    
    private void Initialize()
    {
        IsValid = false;
        IsImportant = Selector.EndsWith('!');
        SelectorSort = 0;

        Wrappers.Clear();
        AllSegments.Clear();
        VariantSegments.Clear();

        AllSegments.AddRange(SplitByColonsRegex().Split(Selector.TrimEnd('!')));

        ProcessVariants();
        
        if (IsValid == false)
            ProcessArbitraryCss();

        if (IsValid == false)
            ProcessUtilityClasses();

        if (IsValid)
        {
            Sb = AppState.StringBuilderPool.Get();
            GenerateSelector();
        }

        if (IsValid)
            GenerateWrappers();
    }
    
    private void ProcessVariants()
    {
        try
        {
            if (AllSegments.Count <= 1)
                return;
            
            // One or more invalid variants invalidate the entire utility class

            foreach (var segment in AllSegments.Take(AllSegments.Count - 1))
            {
                if (string.IsNullOrEmpty(segment))
                    return;

                if (segment.TryVariantIsMediaQuery(AppState, out var mediaQuery))
                {
                    if (mediaQuery is null)
                        return;

                    VariantSegments.Add(segment, mediaQuery);
                }
                else if (segment.TryVariantIsPseudoClass(AppState, out var pseudoClass))
                {
                    if (pseudoClass is null)
                        return;

                    VariantSegments.Add(segment, pseudoClass);
                }
                else if (segment.TryVariantIsGroup(AppState, out var group))
                {
                    if (group is null)
                        return;

                    VariantSegments.Add(segment, group);
                }
                else if (segment.TryVariantIsPeer(AppState, out var peer))
                {
                    if (peer is null)
                        return;

                    VariantSegments.Add(segment, peer);
                }
                else if (segment.TryVariantIsNth(out var nth))
                {
                    if (nth is null)
                        return;

                    VariantSegments.Add(segment, nth);
                }
                else if (segment.TryVariantIsHas(AppState, out var has))
                {
                    if (has is null)
                        return;

                    VariantSegments.Add(segment, has);
                }
                else if (segment.TryVariantIsSupports(AppState, out var supports))
                {
                    if (supports is null)
                        return;

                    VariantSegments.Add(segment, supports);
                }
                else if (segment.TryVariantIsNotSupports(AppState, out var notSupports))
                {
                    if (notSupports is null)
                        return;

                    VariantSegments.Add(segment, notSupports);
                }
                else if (segment.TryVariantIsData(out var data))
                {
                    if (data is null)
                        return;

                    VariantSegments.Add(segment, data);
                }
                else if (segment.TryVariantIsCustom(AppState, out var custom))
                {
                    if (custom is null)
                        return;

                    VariantSegments.Add(segment, custom);
                }
                else
                {
                    return;
                }
            }
        }
        catch
        {
            // Ignored
        }
    }
    
    private void ProcessArbitraryCss()
    {
        try
        {
            if (AllSegments[^1].StartsWith('[') == false || AllSegments[^1].EndsWith(']') == false)
                return;
            
            var trimmedValue = AllSegments[^1].TrimStart('[').TrimEnd(']').Trim('_');
            var colonIndex = trimmedValue.IndexOf(':');

            if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                return;

            if (PatternCssCustomPropertyAssignmentRegex().Match(trimmedValue).Success)
            {
                // [--my-text-size:1rem]
                IsCssCustomPropertyAssignment = true;
                IsValid = true;
                Styles = $"{trimmedValue.Replace('_', ' ').TrimEnd(';')};";
            }
            else if (AppState.Library.CssPropertyNamesWithColons.HasPrefixIn(trimmedValue))
            {
                // [font-size:1rem]
                IsArbitraryCss = true;
                IsValid = true;
                Styles = $"{trimmedValue.Replace('_', ' ').TrimEnd(';')};";
            }

            if (IsValid && IsImportant)
                Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);
        }
        catch
        {
            // Ignored
        }
    }
    
    private void ProcessUtilityClasses()
    {
        try
        {
            var prefix = AppState.Library.ScannerClassNamePrefixes.GetLongestMatchingPrefix(AllSegments[^1]);

            if (string.IsNullOrEmpty(prefix))
                return;

            var value = AllSegments[^1].TrimStart(prefix) ?? string.Empty;
            var slashSegments = SplitBySlashesRegex().Split(value);

            if (slashSegments.Length == 2)
            {
                ModifierValue = slashSegments[^1];
                value = value.TrimEnd($"/{ModifierValue}") ?? string.Empty;
                HasArbitraryModifierValue = ModifierValue.StartsWith('[');
                ModifierValue = ModifierValue.TrimStart('[').TrimEnd(']');
                HasModifierValue = true;
            }

            HasArbitraryValue = value.StartsWith('[') && value.EndsWith(']');
            HasArbitraryValueWithCssCustomProperty = (HasArbitraryValue && value.Contains("--", StringComparison.Ordinal)) || (value.StartsWith('(') && value.EndsWith(')') && value.Contains("--", StringComparison.Ordinal));

            #region Arbitrary Value Using CSS Custom Property, Data Type Prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")

            if (HasArbitraryValueWithCssCustomProperty)
            {
                var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

                if (valueNoBrackets.StartsWith("dimension:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("length:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("percentage:", StringComparison.Ordinal))
                {
                    AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("color:", StringComparison.Ordinal))
                {
                    AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("integer:", StringComparison.Ordinal))
                {
                    AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("alpha:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("number:", StringComparison.Ordinal))
                {
                    AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("image:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("url:", StringComparison.Ordinal))
                {
                    AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("angle:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("hue:", StringComparison.Ordinal))
                {
                    AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("duration:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("time:", StringComparison.Ordinal))
                {
                    AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("flex:", StringComparison.Ordinal))
                {
                    AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("frequency:", StringComparison.Ordinal))
                {
                    AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("ratio:", StringComparison.Ordinal))
                {
                    AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("resolution:", StringComparison.Ordinal))
                {
                    AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("string:", StringComparison.Ordinal))
                {
                    AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is not null)
                {
                    valueNoBrackets = valueNoBrackets[(valueNoBrackets.IndexOf(':') + 1)..];
                    
                    Value = valueNoBrackets.StartsWith("--", StringComparison.Ordinal) ? $"var({valueNoBrackets})" : valueNoBrackets;
                    IsValid = true;
                    SelectorSort = ClassDefinition.SelectorSort;

                    GenerateStyles();

                    return;
                }
            }
            
            #endregion

            #region Arbitrary Value Using CSS Custom Property, No Prefix (e.g. "text-[var(--my-text-size)]" or "text-(--my-text-size)")
            
            if (HasArbitraryValueWithCssCustomProperty && ClassDefinition is null)
            {
                // Iterate through all data type classes to find a prefix match

                var classDictionaries = new List<Dictionary<string, ClassDefinition>>
                {
                    AppState.Library.DimensionLengthClasses,
                    AppState.Library.ColorClasses,
                    AppState.Library.IntegerClasses,
                    AppState.Library.AlphaNumberClasses,
                    AppState.Library.AngleHueClasses,
                    AppState.Library.DurationTimeClasses,
                    AppState.Library.FrequencyClasses,
                    AppState.Library.ImageUrlClasses,
                    AppState.Library.FlexClasses,
                    AppState.Library.RatioClasses,
                    AppState.Library.ResolutionClasses,
                    AppState.Library.StringClasses
                };

                foreach (var dict in classDictionaries)
                {
                    if (dict.TryGetValue(prefix, out ClassDefinition))
                        break;
                }                

                if (ClassDefinition is not null)
                {
                    var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

                    Value = valueNoBrackets.StartsWith("--", StringComparison.Ordinal) ? $"var({valueNoBrackets})" : valueNoBrackets;
                    IsValid = true;
                    SelectorSort = ClassDefinition.SelectorSort;

                    GenerateStyles();

                    return;
                }
            }
            
            #endregion

            #region Auto-Detect Value Data Type (e.g. text-[1rem])
            
            if (HasArbitraryValue && HasArbitraryValueWithCssCustomProperty == false && ClassDefinition is null)
            {
                var valueNoBrackets = value.TrimStart('[').TrimEnd(']');

                if (valueNoBrackets.ValueIsDimensionLength(AppState))
                {
                    AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsFloatNumber())
                {
                    if (AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                        AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.IsValidWebColor())
                {
                    AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsAngleHue(AppState))
                {
                    AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsDurationTime(AppState))
                {
                    AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsFrequency(AppState))
                {
                    AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsImageUrl())
                {
                    AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsRatio())
                {
                    AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.ValueIsResolution(AppState))
                {
                    AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else
                {
                    if (AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition) == false)
                        AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is not null)
                {
                    if (ClassDefinition.UsesColor && HasModifierValue)
                    {
                        if (int.TryParse(ModifierValue, out var alphaPct))
                            Value = valueNoBrackets.SetWebColorAlpha(alphaPct);
                        else if (double.TryParse(ModifierValue, out var alpha))
                            Value = valueNoBrackets.SetWebColorAlpha(alpha);
                        else
                            Value = valueNoBrackets;                    }
                    else
                    {
                        Value = valueNoBrackets;
                    }

                    IsValid = true;
                    SelectorSort = ClassDefinition.SelectorSort;

                    GenerateStyles();

                    return;
                }
            }
            
            #endregion
            
            #region Static, Spacing, or Color Name Value

            if (HasArbitraryValue || ClassDefinition is not null)
                return;
            
            if (string.IsNullOrEmpty(value))
            {
                AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);
            }
            else if (value.ValueIsInteger())
            {
                if (AppState.Library.SpacingClasses.TryGetValue(prefix, out ClassDefinition))
                    Value = value;
            }
            else if (value.ValueIsColorName(AppState))
            {
                if (AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition))
                {
                    if (AppState.Library.ColorsByName.TryGetValue(value, out var colorValue))
                    {
                        if (HasModifierValue)
                        {
                            if (int.TryParse(ModifierValue, out var alphaPct))
                                Value = colorValue.SetWebColorAlpha(alphaPct);
                            else if (double.TryParse(ModifierValue, out var alpha))
                                Value = colorValue.SetWebColorAlpha(alpha);
                            else
                                Value = colorValue;                        }
                        else
                        {
                            Value = colorValue;
                        }
                    }
                }
            }

            if (ClassDefinition is null)
                return;

            IsValid = true;
            SelectorSort = ClassDefinition.SelectorSort;

            GenerateStyles();

            #endregion
        }
        catch
        {
            // Ignored
        }
    }
   
    private void GenerateSelector()
    {
        Sb?.Clear();
        
        try
        {
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "prefix").OrderByDescending(s => s.Value.PrefixOrder))
                Sb?.Append(variant.Value.SelectorPrefix);

            Sb?.Append('.');
            Sb?.Append(Selector.CssSelectorEscape());
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "pseudoclass").OrderByDescending(s => s.Value.PrefixOrder))
                Sb?.Append(variant.Value.SelectorSuffix);
            
            EscapedSelector = Sb?.ToString() ?? string.Empty;
        }
        catch
        {
            IsValid = false;
        }
    }

    private void GenerateWrappers()
    {
        Sb?.Clear();

        try
        {
            if (VariantSegments.TryGetValue("dark", out var darkVariant))
            {
                Wrappers.Add($"@{darkVariant.PrefixType} {darkVariant.Statement} {{");
            }

            foreach (var queryType in new[] { "media", "supports" })
            {
                foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == queryType && s.Key != "dark").OrderByDescending(s => s.Value.PrefixOrder))
                {
                    if (Sb?.Length == 0)
                        Sb.Append($"@{queryType} ");
                    else
                        Sb?.Append(" and ");
                
                    Sb?.Append(variant.Value.Statement);
                }

                if (Sb?.Length <= 0)
                    continue;
                
                Sb?.Append(" {");
                Wrappers.Add(Sb?.ToString() ?? string.Empty);
                Sb?.Clear();
            }

            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType is "wrapper").OrderByDescending(s => s.Value.PrefixOrder))
            {
                Wrappers.Add($"{variant.Value.Statement} {{");
            }
        }
        catch
        {
            IsValid = false;
        }
    }

    private void GenerateStyles()
    {
        if (HasArbitraryValue && string.IsNullOrEmpty(ClassDefinition?.ArbitraryCssValueTemplate) == false)
        {
            Styles = ClassDefinition.ArbitraryCssValueTemplate;
        }
        else if (HasModifierValue)
        {
            if (string.IsNullOrEmpty(ClassDefinition?.ModifierTemplate) == false)
                Styles = ClassDefinition.ModifierTemplate;
            else
                Styles = ClassDefinition?.Template ?? string.Empty;
            
            if (HasArbitraryModifierValue)
            {
                if (string.IsNullOrEmpty(ClassDefinition?.ArbitraryModifierTemplate) == false)
                    Styles = ClassDefinition.ArbitraryModifierTemplate;
            }
        }
        else
        {
            Styles = ClassDefinition?.Template ?? string.Empty;
        }

        Styles = Styles.Replace("{0}", Value, StringComparison.Ordinal);

        if (HasModifierValue)
            Styles = Styles.Replace("{1}", ModifierValue, StringComparison.Ordinal);
            
        if (IsImportant)
            Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);
    }

    #endregion
}