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

    private StringBuilder Sb1 { get; set; } = null!;
    private StringBuilder Sb2 { get; set; } = null!;

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
        AppState.StringBuilderPool.Return(Sb1);
        AppState.StringBuilderPool.Return(Sb2);
    }

    #endregion

    // todo: create code to iterate, group, wrap classes, generate actual CSS (perhaps create a list of segments based on like media queries, then stack them in the final CSS)

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

        if (IsValid == false)
            return;
                
        Sb1 = AppState.StringBuilderPool.Get();
        Sb2 = AppState.StringBuilderPool.Get();

        GenerateSelector();
        GenerateWrappers();
    }
    
    private void ProcessVariants()
    {
        try
        {
            #region Process Variants

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

            #endregion
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
            #region Process Arbitrary CSS

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

            #endregion
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
            #region Process Utility Classes

            var prefix = AppState.Library.ScannerClassNamePrefixes.GetLongestMatchingPrefix(AllSegments[^1]);

            if (string.IsNullOrEmpty(prefix))
                return;

            var value = AllSegments[^1].TrimStart(prefix) ?? string.Empty;
            var slashSegments = SplitBySlashesRegex().Split(value);

            if (slashSegments.Length == 2)
            {
                ModifierValue = slashSegments[^1];
                value = value.TrimEnd($"/{ModifierValue}") ?? string.Empty;
                ModifierValue = ModifierValue.TrimStart('[').TrimEnd(']');
            }

            if ((value.StartsWith('[') && value.EndsWith(']')) || (value.StartsWith('(') && value.EndsWith(')')))
            {
                var customValue = value.TrimStart('[').TrimStart('(').TrimEnd(']').TrimEnd(')');

                // Specified arbitrary value data type prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")

                if (customValue.StartsWith("dimension:", StringComparison.Ordinal) || customValue.StartsWith("length:", StringComparison.Ordinal) || customValue.StartsWith("percentage:", StringComparison.Ordinal))
                {
                    AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("color:", StringComparison.Ordinal))
                {
                    AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("integer:", StringComparison.Ordinal))
                {
                    AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("alpha:", StringComparison.Ordinal) || customValue.StartsWith("number:", StringComparison.Ordinal))
                {
                    AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("image:", StringComparison.Ordinal) || customValue.StartsWith("url:", StringComparison.Ordinal))
                {
                    AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("angle:", StringComparison.Ordinal) || customValue.StartsWith("hue:", StringComparison.Ordinal))
                {
                    AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("duration:", StringComparison.Ordinal) || customValue.StartsWith("time:", StringComparison.Ordinal))
                {
                    AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("flex:", StringComparison.Ordinal))
                {
                    AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("frequency:", StringComparison.Ordinal))
                {
                    AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("ratio:", StringComparison.Ordinal))
                {
                    AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("resolution:", StringComparison.Ordinal))
                {
                    AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (customValue.StartsWith("string:", StringComparison.Ordinal))
                {
                    AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is not null)
                {
                    Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                }
                else
                {
                    // Did not specify data type prefix (e.g. "text-[var(--my-text-size)]" or "text-(--my-text-size)")
                    // Iterate through all data type classes to find a prefix match
                    
                    if (value.StartsWith("(--", StringComparison.Ordinal) || value.StartsWith("[var(--", StringComparison.Ordinal))
                    {
                        AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);

                        if (ClassDefinition is null)
                        {
                            AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                        }

                        if (ClassDefinition is null)
                        {
                            AppState.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                    }
                    
                    if (ClassDefinition is not null)
                    {
                        Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                    }
                    else
                    {
                        // Auto-detect value type

                        if (customValue.ValueIsDimensionLength(AppState))
                        {
                            AppState.Library.DimensionLengthClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsFloatNumber())
                        {
                            if (AppState.Library.AlphaNumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                                AppState.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.IsValidWebColor())
                        {
                            AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsAngleHue(AppState))
                        {
                            AppState.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsDurationTime(AppState))
                        {
                            AppState.Library.DurationTimeClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsFrequency(AppState))
                        {
                            AppState.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsImageUrl())
                        {
                            AppState.Library.ImageUrlClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsRatio())
                        {
                            AppState.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                        }
                        else if (customValue.ValueIsResolution(AppState))
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
                            Value = customValue.StartsWith("--", StringComparison.Ordinal) ? $"var({customValue})" : customValue;
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    AppState.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (value.ValueIsInteger())
                {
                    AppState.Library.SpacingClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (value.ValueIsColorName(AppState))
                {
                    AppState.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is not null)
                {
                    if (ClassDefinition.UsesColor && AppState.Library.Colors.TryGetValue(value, out var colorValue))
                        Value = colorValue;
                    else
                        Value = value;
                }
                else
                {
                    Value = value;
                }
            }

            if (ClassDefinition is null)
                return;
            
            IsValid = true;
            SelectorSort = ClassDefinition.SelectorSort;

            if (string.IsNullOrEmpty(ModifierValue) || string.IsNullOrEmpty(ClassDefinition.ModifierTemplate))
            {
                Styles = ClassDefinition.Template
                    .Replace("{0}", Value, StringComparison.Ordinal);

                if (string.IsNullOrEmpty(ModifierValue) == false)
                {
                    Styles = Styles
                        .Replace("{1}", ModifierValue, StringComparison.Ordinal);
                }
            }
            else
            {
                Styles = ClassDefinition.ModifierTemplate
                    .Replace("{0}", Value, StringComparison.Ordinal)
                    .Replace("{1}", ModifierValue, StringComparison.Ordinal);
            }

            if (IsImportant)
                Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);

            #endregion
        }
        catch
        {
            // Ignored
        }
    }

    private void GenerateSelector()
    {
        Sb1.Clear();
        Sb2.Clear();
        
        try
        {
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "prefix").OrderByDescending(s => s.Value.PrefixOrder))
                Sb1.Append(variant.Value.SelectorPrefix);

            Sb1.Append(Selector.CssSelectorEscape(Sb2));
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "pseudoclass").OrderByDescending(s => s.Value.PrefixOrder))
                Sb1.Append(variant.Value.SelectorSuffix);
            
            EscapedSelector = Sb1.ToString();
        }
        catch
        {
            // Ignored
        }
    }

    private void GenerateWrappers()
    {
        Sb1.Clear();

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
                    if (Sb1.Length == 0)
                        Sb1.Append($"@{queryType} ");
                    else
                        Sb1.Append(" and ");
                
                    Sb1.Append(variant.Value.Statement);
                }

                if (Sb1.Length <= 0)
                    continue;
                
                Sb1.Append(" {");
                Wrappers.Add(Sb1.ToString());
                Sb1.Clear();
            }

            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType is "wrapper").OrderByDescending(s => s.Value.PrefixOrder))
            {
                Wrappers.Add($"{variant.Value.Statement} {{");
            }
        }
        catch
        {
            // Ignored
        }
    }
    
    #endregion
}