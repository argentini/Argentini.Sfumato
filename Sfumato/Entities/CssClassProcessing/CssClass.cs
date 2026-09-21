// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantBoolCompare
// ReSharper disable ForCanBeConvertedToForeach

using Sfumato.Entities.Scanning;
using Sfumato.Validators;
using Sfumato.Entities.Runners;
using Sfumato.Entities.Trie;
using Sfumato.Entities.UtilityClasses;
using System.Globalization;

namespace Sfumato.Entities.CssClassProcessing;

public sealed class CssClass : IDisposable
{
    #region Properties
    
    public AppRunner AppRunner { get; set; }
    
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
    public Dictionary<string, VariantMetadata> VariantSegments { get; } = new (StringComparer.Ordinal);

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition;

    /// <summary>
    /// Ordered list of nested wrapper statements (for variants)
    /// </summary>
    public Dictionary<ulong, string> Wrappers { get; } = [];

    private string? _prefix;
    public string EscapedSelector { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ModifierValue { get; set; } = string.Empty;
    public string Styles { get; set; } = string.Empty;

    public int SelectorSort { get; set; }
    public int WrapperSort { get; set; }

    public bool IsValid { get; set; }
    public bool IsArbitraryCss { get; set; }
    public bool IsCssCustomPropertyAssignment { get; set; }
    public bool IsImportant { get; set; }
    public bool HasModifierValue { get; set; }
    public bool HasArbitraryModifierValue { get; set; }
    public bool HasArbitraryValue { get; set; }
    public bool HasArbitraryValueWithCssCustomProperty { get; set; }
    public bool UsesDarkTheme { get; set; }
    public bool HasRazorSyntax { get; set; }

    public StringBuilder? Sb { get; set; }
    public StringBuilder? WorkingSb { get; set; }

    #endregion
    
    #region Lifecycle

    public CssClass(AppRunner appRunner, string? prefix = null)
    {
        AppRunner = appRunner;
        Selector = string.Empty;
        _prefix = prefix;
    }

    public CssClass(AppRunner appRunner, string selector, string? prefix = null)
    {
        AppRunner = appRunner;
        Selector = selector;
        _prefix = prefix;

        Initialize();
    }

    public void Dispose()
    {
        if (Sb is not null)
            AppRunner.StringBuilderPool.Return(Sb);
        
        if (WorkingSb is not null)
            AppRunner.StringBuilderPool.Return(WorkingSb);
    }

    #endregion

    #region Initialization

    public bool ProcessSelectorSegments()
    {
        return CssCandidateParser.TryResolveSimple(this);
    }
    
    public void Initialize()
    {
        CssCompiler.Compile(this);
    }
    
    #endregion
    
    #region Processing
    
    public void ProcessVariants()
    {
        CssVariantResolver.Resolve(this);
    }
    
    public void ProcessArbitraryCss()
    {
        CssCandidateParser.ResolveArbitrary(this);
    }
    
    public void ProcessUtilityClasses() => CssUtilityResolver.Resolve(this);

    internal void ResolveUtilityCore()
    {
        try
        {
            if (_prefix is null)
            {
                var slashIndex = AllSegments[^1].IndexOf('/');
                
                AppRunner.Library.ScannerClassNamePrefixes.TryGetLongestMatchingPrefix(slashIndex  > -1 ? AllSegments[^1][..slashIndex] : AllSegments[^1], out _prefix, out _);
            }

            if (string.IsNullOrEmpty(_prefix))
                return;

            var value = AllSegments[^1].TrimStart(_prefix) ?? string.Empty;

            if (AppRunner.Library.FunctionalClasses.Count > 0 && AppRunner.Library.FunctionalClasses.TryGetValue(_prefix, out var functionalUtilities))
            {
                var stylesBuilder = AppRunner.StringBuilderPool.Get();

                try
                {
                    for (var index = 0; index < functionalUtilities.Count; index++)
                    {
                        if (functionalUtilities[index].TryResolve(value, AppRunner, out var resolvedStyles) == false)
                            continue;

                        if (stylesBuilder.Length > 0)
                            stylesBuilder.Append(AppRunner.AppRunnerSettings.LineBreak);

                        stylesBuilder.Append(resolvedStyles);
                    }

                    if (stylesBuilder.Length > 0)
                    {
                        ClassDefinition = new ClassDefinition
                        {
                            InSimpleUtilityCollection = true,
                            Template = stylesBuilder.ToString(),
                        };
                        IsValid = true;
                        GenerateStyles();
                    }
                }
                finally
                {
                    AppRunner.StringBuilderPool.Return(stylesBuilder);
                }

                return;
            }

            if (value.Contains('/'))
            {
                var slashCount = 0;
                var modifier = string.Empty;

                foreach (var segment in value.SplitByTopLevel('/'))
                {
                    if (slashCount == 1)
                        modifier = segment.ToString();

                    slashCount++;
                }

                if (slashCount == 2)
                {
                    ModifierValue = modifier;
                    value = value.TrimEnd($"/{ModifierValue}") ?? string.Empty;
                    HasArbitraryModifierValue = ModifierValue.StartsWith('[');
                    ModifierValue = ModifierValue.TrimStart('[').TrimEnd(']');

                    if (string.IsNullOrEmpty(ModifierValue) == false)
                    {
                        if (ModifierValue[0] is 'l' or 's' or 'i' or 'd')
                        {
                            ModifierValue = ModifierValue switch
                            {
                                "longer" => "oklch longer hue",
                                "shorter" => "oklch shorter hue",
                                "increasing" => "oklch increasing hue",
                                "decreasing" => "oklch decreasing hue",
                                _ => ModifierValue
                            };
                        }

                        HasModifierValue = true;
                    }
                }
            }

            #region Handle Simple Classes
            
            if (string.IsNullOrEmpty(value))
            {
                if (AppRunner.Library.SimpleClasses.TryGetValue(_prefix, out ClassDefinition))
                {
                    if (HasModifierValue && ClassDefinition.HasBehavior(UtilityBehaviors.SlashModifier) == false)
                    {
                        ClassDefinition = null;
                        return;
                    }

                    IsValid = true;
                    SelectorSort = ClassDefinition.SelectorSort;

                    GenerateStyles();

                    return;
                }
            }
            
            #endregion

            HasArbitraryValue = value.StartsWith('[');
            HasArbitraryValueWithCssCustomProperty = (HasArbitraryValue && value.Contains("--", StringComparison.Ordinal)) || (value.StartsWith('(') && value.EndsWith(')') && value.Contains("--", StringComparison.Ordinal));

            #region Handle Fractions/Ratios
            
            if (HasArbitraryValue == false && HasArbitraryValueWithCssCustomProperty == false && HasModifierValue && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var numerator) && double.TryParse(ModifierValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var denominator))
            {
                if (denominator != 0 && IsValidFractionPart(value, numerator) && IsValidFractionPart(ModifierValue, denominator))
                {
                    if (AppRunner.Library.RatioClasses.TryGetValue(_prefix, out ClassDefinition))
                    {
                        if (ClassDefinition.HasBehavior(UtilityBehaviors.SlashModifier) == false)
                            Value = $"{value} / {ModifierValue}";
                    }
                    else if (AppRunner.Library.LengthClasses.TryGetValue(_prefix, out ClassDefinition) || AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition))
                    {
                        if (ClassDefinition.HasBehavior(UtilityBehaviors.DisallowFractions))
                            ClassDefinition = null;
                        else if (ClassDefinition.HasBehavior(UtilityBehaviors.SlashModifier) == false)
                            Value = $"{((double)numerator / denominator * 100).ToString("0.############", CultureInfo.InvariantCulture)}%";
                    }

                    if (ClassDefinition?.HasBehavior(UtilityBehaviors.SlashModifier) ?? false)
                    {
                        ClassDefinition = null;
                    }
                    
                    if (ClassDefinition is not null)
                    {
                        IsValid = true;
                        SelectorSort = ClassDefinition.SelectorSort;

                        GenerateStyles(true);

                        return;
                    }
                }
            }
            
            #endregion
            
            #region Arbitrary Value Using CSS Custom Property, Data Type Prefix (e.g. "text-[length:var(--my-text-size)]" or "text-(length:--my-text-size)")

            if (HasArbitraryValueWithCssCustomProperty && ClassDefinition is null)
            {
                var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(')').TrimEnd(']').ProcessUnderscores();
                var typeSeparator = valueNoBrackets.IndexOf(':');
                var valueKind = typeSeparator < 0
                    ? UtilityValueKinds.Abstract
                    : valueNoBrackets.AsSpan(0, typeSeparator) switch
                    {
                        "dimension" or "length" => UtilityValueKinds.Length,
                        "color" => UtilityValueKinds.Color,
                        "integer" => UtilityValueKinds.Integer,
                        "percentage" => UtilityValueKinds.Percentage,
                        "alpha" or "number" => UtilityValueKinds.Number,
                        "image" or "url" => UtilityValueKinds.Url,
                        "angle" or "hue" => UtilityValueKinds.Angle,
                        "duration" or "time" => UtilityValueKinds.Duration,
                        "flex" => UtilityValueKinds.Flex,
                        "frequency" => UtilityValueKinds.Frequency,
                        "ratio" => UtilityValueKinds.Ratio,
                        "resolution" => UtilityValueKinds.Resolution,
                        "string" => UtilityValueKinds.String,
                        _ => UtilityValueKinds.Abstract,
                    };

                AppRunner.Library.TryGetClassDefinition(_prefix, valueKind, out ClassDefinition);

                if (ClassDefinition is not null)
                {
                    valueNoBrackets = valueNoBrackets[(typeSeparator + 1)..];
                    
                    Value = valueNoBrackets.StartsWith("--", StringComparison.Ordinal) && valueNoBrackets.Contains('(') == false
                        ? $"var({valueNoBrackets})"
                        : valueNoBrackets;
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
                AppRunner.Library.TryGetUntypedCustomPropertyDefinition(_prefix, out ClassDefinition);

                if (ClassDefinition is not null)
                {
                    var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(')').TrimEnd(']').ProcessUnderscores();

                    Value = valueNoBrackets.StartsWith("--", StringComparison.Ordinal) && valueNoBrackets.Contains('(') == false
                        ? $"var({valueNoBrackets})"
                        : valueNoBrackets;
                    IsValid = true;
                    SelectorSort = ClassDefinition.SelectorSort;

                    GenerateStyles();

                    return;
                }
            }
            
            #endregion

            #region Auto-Detect Arbitrary Value Data Type (e.g. text-[1rem])
            
            if (HasArbitraryValue && HasArbitraryValueWithCssCustomProperty == false && ClassDefinition is null)
            {
                var valueNoBrackets = value.TrimStart('[').TrimEnd(']').ProcessUnderscores();

                if (valueNoBrackets.ValueIsPercentage())
                {
                    AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsDimensionLength(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal) || valueNoBrackets.StartsWith("env(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.LengthClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && valueNoBrackets.ValueIsFloatNumber())
                {
                    if (AppRunner.Library.FloatNumberClasses.TryGetValue(_prefix, out ClassDefinition) == false)
                        AppRunner.Library.IntegerClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && valueNoBrackets.IsValidWebColor())
                {
                    AppRunner.Library.ColorClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsAngleHue(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.AngleHueClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsDurationTime(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.DurationClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsFrequency(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.FrequencyClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && valueNoBrackets.ValueIsUrl())
                {
                    AppRunner.Library.UrlClasses.TryGetValue(_prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppRunner.Library.AbstractClasses.TryGetValue(_prefix, out ClassDefinition);
                    else if (valueNoBrackets.StartsWith("url(", StringComparison.Ordinal) == false)
                        valueNoBrackets = $"url({valueNoBrackets})";
                }

                if (ClassDefinition is null && valueNoBrackets.ValueIsRatio())
                {
                    AppRunner.Library.RatioClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && (valueNoBrackets.ValueIsResolution(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.ResolutionClasses.TryGetValue(_prefix, out ClassDefinition);
                }

                if (ClassDefinition is null)
                {
                    if (AppRunner.Library.FlexClasses.TryGetValue(_prefix, out ClassDefinition) == false)
                    {
                        if (AppRunner.Library.AbstractClasses.TryGetValue(_prefix, out ClassDefinition) == false)
                            AppRunner.Library.StringClasses.TryGetValue(_prefix, out ClassDefinition);
                    }
                }
                
                if (ClassDefinition is not null)
                {
                    if (ClassDefinition.Accepts(UtilityValueKinds.Color) && HasModifierValue)
                    {
                        if (TryResolveOpacityPercentage(out var alphaPct) == false)
                            return;

                        Value = valueNoBrackets.SetWebColorAlphaByPercentage(alphaPct);
                    }
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
            
            #region Static, Numeric Suffix, or Color Name Value

            if (HasArbitraryValue || ClassDefinition is not null)
                return;

            if (value.ValueIsFloatNumber())
            {
                AppRunner.Library.LengthClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition?.HasBehavior(UtilityBehaviors.ArbitraryLengthOnly) ?? false)
                    ClassDefinition = null;

                if (ClassDefinition is null && int.TryParse(value, out _))
                    AppRunner.Library.IntegerClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FloatNumberClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FlexClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.AngleHueClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.DurationClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FrequencyClasses.TryGetValue(_prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.ResolutionClasses.TryGetValue(_prefix, out ClassDefinition);
            }

            if (ClassDefinition is null && value.ValueIsPercentage())
                AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition);

            if (ClassDefinition is not null)
                Value = value;

            if (ClassDefinition is null && value.ValueIsColorName(AppRunner))
            {
                if (AppRunner.Library.ColorClasses.TryGetValue(_prefix, out ClassDefinition))
                {
                    if (AppRunner.Library.ColorsByName.TryGetValue(value, out var colorValue))
                    {
                        if (HasModifierValue)
                        {
                            if (TryResolveOpacityPercentage(out var pct) == false)
                                return;
                            
                            if (pct < 100)
                            {
                                if (AppRunner.AppRunnerSettings.UseCompatibilityMode)
                                {
                                    Value = colorValue.SetWebColorAlphaByPercentage(pct);
                                }
                                else
                                {
                                    if (colorValue.Contains("oklch", StringComparison.Ordinal))
                                        Value = $"color-mix(in oklab, var(--color-{value}) {pct}%, transparent)";
                                    else if (colorValue.Contains("rgb", StringComparison.Ordinal) || colorValue.Contains('#'))
                                        Value = $"color-mix(in srgb, var(--color-{value}) {pct}%, transparent)";
                                    else
                                    {
                                        var colorSpace = AppRunner.Library.ColorSpaces.FirstOrDefault(c => colorValue.Contains(c));

                                        Value = colorSpace is not null ? $"color-mix(in {colorSpace}, var(--color-{value}) {pct}%, transparent)" : colorValue.SetWebColorAlphaByPercentage(pct);
                                    }
                                }
                            }
                            else
                            {
                                Value = $"var(--color-{value})";
                            }
                        }
                        else
                        {
                            Value = $"var(--color-{value})";
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
            IsValid = false;
        }

        return;

        static bool IsValidFractionPart(string text, double number)
        {
            return number >= 0 && number % 0.25 == 0 && number.ToString("0.############", CultureInfo.InvariantCulture) == text;
        }
    }

    private bool TryResolveOpacityPercentage(out double percentage)
    {
        percentage = 0;

        if (HasArbitraryModifierValue)
        {
            if (ModifierValue.EndsWith('%'))
                return double.TryParse(ModifierValue.AsSpan(0, ModifierValue.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture, out percentage);

            if (double.TryParse(ModifierValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var alpha) == false)
                return false;

            percentage = alpha * 100;
            return true;
        }

        if (double.TryParse(ModifierValue, NumberStyles.Float, CultureInfo.InvariantCulture, out percentage) == false)
            return false;

        return percentage >= 0
            && percentage % 0.25 == 0
            && percentage.ToString("0.############", CultureInfo.InvariantCulture) == ModifierValue;
    }

    public void GenerateSelector() => CssRuleEmitter.GenerateSelector(this);

    internal void GenerateSelectorCore()
    {
        try
        {
            // Cache count to avoid repeated property access
            var variantCount = VariantSegments.Count;

            if (HasRazorSyntax)
            {
                Selector = Selector.ConsolidateAtSymbols();
                HasRazorSyntax = false;
            }

            EscapedSelector = Selector.CssSelectorEscape();
            
            if (variantCount == 0)
                return;

            Sb ??= AppRunner.StringBuilderPool.Get();
            Sb.Clear();

            // Single pass through variants to categorize and check for descendants
            var prefixVariants = new List<KeyValuePair<string, VariantMetadata>>(variantCount);
            var pseudoclassVariants = new List<KeyValuePair<string, VariantMetadata>>(variantCount);
            var hasDescendantVariant = false;
            var hasStarPseudoclass = false;
            var hasDoubleStarPseudoclass = false;
            var hasInheritedPseudoclass = false;

            foreach (var variant in VariantSegments)
            {
                var key = variant.Key;
                var metadata = variant.Value;

                // Check for descendant variants
                if (key[0] == '*')
                    hasDescendantVariant = true;

                // Categorize by prefix type
                if (metadata.PrefixType == "prefix")
                {
                    prefixVariants.Add(variant);
                }
                else if (metadata.PrefixType == "pseudoclass")
                {
                    pseudoclassVariants.Add(variant);

                    if (hasDescendantVariant == false)
                        continue;
                    
                    if (key.Length > 1 && key[1] == '*')
                        hasDoubleStarPseudoclass = true;
                    else
                        hasStarPseudoclass = true;
                }
            }

            // Sort once instead of using OrderByDescending in loops
            if (prefixVariants.Count > 1)
                prefixVariants.Sort((a, b) => b.Value.PrefixOrder.CompareTo(a.Value.PrefixOrder));

            if (pseudoclassVariants.Count > 1)
                pseudoclassVariants.Sort((a, b) => b.Value.PrefixOrder.CompareTo(a.Value.PrefixOrder));

            if (hasDescendantVariant)
                Sb.Append(":is(");

            // Append prefix variants
            foreach (var variant in prefixVariants)
                Sb.Append(variant.Value.SelectorPrefix);

            Sb.Append(EscapedSelector);

            // Append pseudoclass variants
            foreach (var kvp in pseudoclassVariants)
            {
                Sb.Append(kvp.Value.SelectorSuffix);

                if (kvp.Value.Inheritable)
                {
                    hasInheritedPseudoclass = true;
                    WorkingSb ??= AppRunner.StringBuilderPool.Get();
                    WorkingSb.ReplaceContent($", {EscapedSelector} {kvp.Value.SelectorSuffix}");
                }
            }

            if (hasDescendantVariant)
            {
                if (hasStarPseudoclass)
                    Sb.Append(" > *)");
                else if (hasDoubleStarPseudoclass)
                    Sb.Append(" *)");
            }

            if (hasInheritedPseudoclass)
                Sb.Append(WorkingSb);
            
            EscapedSelector = Sb.ToString();
        }
        catch
        {
            IsValid = false;
            EscapedSelector = string.Empty;
        }
    }

    public void GenerateWrappers() => CssRuleEmitter.GenerateWrappers(this);

    internal void GenerateWrappersCore()
    {
        var variantCount = VariantSegments.Count;

        if (variantCount == 0)
            return;

        Sb ??= AppRunner.StringBuilderPool.Get();

        try
        {
            Sb.Clear();
            Wrappers.Clear();

            var mediaVariants = new KeyValuePair<string, VariantMetadata>[variantCount];
            var supportsVariants = new KeyValuePair<string, VariantMetadata>[variantCount];
            var startingStyleVariants = new KeyValuePair<string, VariantMetadata>[variantCount];
            var containerVariants = new KeyValuePair<string, VariantMetadata>[variantCount];
            var wrapperVariants = new KeyValuePair<string, VariantMetadata>[variantCount];

            int mediaCount = 0, supportsCount = 0, startingStyleCount = 0, containerCount = 0, wrapperCount = 0;
            VariantMetadata? darkVariant = null;

            foreach (var variant in VariantSegments)
            {
                switch (variant.Value.PrefixType)
                {
                    case "media" when variant.Key == "dark":
                        darkVariant = variant.Value;
                        break;
                    case "media":
                        mediaVariants[mediaCount++] = variant;
                        break;
                    case "supports":
                        supportsVariants[supportsCount++] = variant;
                        break;
                    case "starting-style":
                        startingStyleVariants[startingStyleCount++] = variant;
                        break;
                    case "container":
                        containerVariants[containerCount++] = variant;
                        break;
                    case "wrapper":
                        wrapperVariants[wrapperCount++] = variant;
                        break;
                }
            }

            if (darkVariant is not null)
            {
                var wrapper = $"@media {darkVariant.Statement} {{";

                Wrappers.Add(wrapper.Fnv1AHash64(), wrapper);
                UsesDarkTheme = true;
                WrapperSort += darkVariant.PrefixOrder;
            }

            // Sort only the used portions of arrays
            if (mediaCount > 1)
                Array.Sort(mediaVariants, 0, mediaCount, PrefixOrderComparer);
            if (supportsCount > 1)
                Array.Sort(supportsVariants, 0, supportsCount, PrefixOrderComparer);
            if (startingStyleCount > 1)
                Array.Sort(startingStyleVariants, 0, startingStyleCount, PrefixOrderComparer);
            if (containerCount > 1)
                Array.Sort(containerVariants, 0, containerCount, PrefixOrderComparer);
            if (wrapperCount > 1)
                Array.Sort(wrapperVariants, 0, wrapperCount, PrefixOrderComparer);

            // Process variants using spans for zero-copy slicing
            ProcessQueryVariants(mediaVariants.AsSpan(0, mediaCount), "media");
            ProcessQueryVariants(supportsVariants.AsSpan(0, supportsCount), "supports");
            ProcessQueryVariants(startingStyleVariants.AsSpan(0, startingStyleCount), "starting-style");

            if (containerCount > 0)
                ProcessContainerVariants(containerVariants.AsSpan(0, containerCount));

            // Process wrapper variants
            for (var i = 0; i < wrapperCount; i++)
            {
                var wrapper = $"{wrapperVariants[i].Value.Statement} {{";
                Wrappers.Add(wrapper.Fnv1AHash64(), wrapper);
            }
        }
        catch
        {
            IsValid = false;
            Wrappers.Clear();
        }
    }

    // Cache the comparer to avoid allocating it repeatedly
    private static readonly IComparer<KeyValuePair<string, VariantMetadata>> PrefixOrderComparer = Comparer<KeyValuePair<string, VariantMetadata>>.Create((a, b) => a.Value.PrefixOrder.CompareTo(b.Value.PrefixOrder));

    private void ProcessQueryVariants(ReadOnlySpan<KeyValuePair<string, VariantMetadata>> variants, string queryType)
    {
        if (variants.Length == 0)
            return;

        Sb ??= AppRunner.StringBuilderPool.Get();

        Sb.Clear();
        Sb.Append('@');
        Sb.Append(queryType);

        if (string.IsNullOrEmpty(variants[0].Value.Statement) == false)
            Sb.Append(' ');

        Sb.Append(variants[0].Value.Statement);

        if (queryType == "media" && WrapperSort <= int.MaxValue - variants[0].Value.PrefixOrder)
            WrapperSort += variants[0].Value.PrefixOrder;

        for (var i = 1; i < variants.Length; i++)
        {
            Sb.Append(" and ");
            Sb.Append(variants[i].Value.Statement);

            if (queryType == "media" && WrapperSort < int.MaxValue)
                WrapperSort += 1;
        }

        Sb.Append(" {");
        Wrappers.Add(Sb.Fnv1AHash64(), Sb.ToString());
    }

    private void ProcessContainerVariants(ReadOnlySpan<KeyValuePair<string, VariantMetadata>> containerVariants)
    {
        Sb ??= AppRunner.StringBuilderPool.Get();

        Sb.Clear();
        Sb.Append("@container ");

        var firstVariant = containerVariants[0];
        var indexOfSlash = firstVariant.Key.LastIndexOfTopLevel('/');

        if (indexOfSlash > 0)
        {
            Sb.Append(firstVariant.Key.AsSpan(indexOfSlash + 1));
            Sb.Append(' ');
        }

        Sb.Append(firstVariant.Value.Statement);

        for (var i = 1; i < containerVariants.Length; i++)
        {
            Sb.Append(" and ");
            Sb.Append(containerVariants[i].Value.Statement);
        }

        Sb.Append(" {");
        Wrappers.Add(Sb.Fnv1AHash64(), Sb.ToString());
    }

    public void GenerateStyles(bool useArbitraryValue = false) => CssRuleEmitter.GenerateStyles(this, useArbitraryValue);

    internal void GenerateStylesCore(bool useArbitraryValue = false)
    {
        if (ClassDefinition is null)
            return;

        if (HasModifierValue && ClassDefinition.HasBehavior(UtilityBehaviors.SlashModifier) == false && ClassDefinition.Accepts(UtilityValueKinds.Color) == false && useArbitraryValue == false)
        {
            IsValid = false;
            Styles = string.Empty;
            return;
        }

        var opacityPercentage = 0d;

        if (HasModifierValue && ClassDefinition.HasBehavior(UtilityBehaviors.OpacityModifier) && TryResolveOpacityPercentage(out opacityPercentage) == false)
        {
            IsValid = false;
            Styles = string.Empty;
            return;
        }

        if (HasModifierValue && ClassDefinition.HasBehavior(UtilityBehaviors.OpacityModifier) && HasArbitraryModifierValue)
            ModifierValue = $"{opacityPercentage.ToString("0.############", CultureInfo.InvariantCulture)}%";

        Styles = ClassDefinition.Template;

        if (HasArbitraryValue || HasArbitraryValueWithCssCustomProperty || useArbitraryValue)
        {
            if (string.IsNullOrEmpty(ClassDefinition.ArbitraryCssValueTemplate) == false)
                Styles = ClassDefinition.ArbitraryCssValueTemplate;

            if (HasModifierValue)
            {
                if (string.IsNullOrEmpty(ClassDefinition.ArbitraryCssValueWithModifierTemplate) == false)
                    Styles = ClassDefinition.ArbitraryCssValueWithModifierTemplate;

                if (HasArbitraryModifierValue)
                {
                    if (string.IsNullOrEmpty(ClassDefinition.ArbitraryCssValueWithArbitraryModifierTemplate) == false)
                        Styles = ClassDefinition.ArbitraryCssValueWithArbitraryModifierTemplate;
                }
            }

            Styles = Styles.Replace("{0}", Value.Contains("(--", StringComparison.OrdinalIgnoreCase) ? Value.Replace("var(--", "(--").Replace("(--", "var(--") : Value, StringComparison.Ordinal);
        }
        else
        {
            if (HasModifierValue)
            {
                if (string.IsNullOrEmpty(ClassDefinition.ModifierTemplate) == false)
                    Styles = ClassDefinition.ModifierTemplate;

                if (HasArbitraryModifierValue)
                {
                    if (string.IsNullOrEmpty(ClassDefinition.ArbitraryModifierTemplate) == false)
                        Styles = ClassDefinition.ArbitraryModifierTemplate;
                }
            }

            Styles = Styles.Replace("{0}", Value, StringComparison.Ordinal);
        }

        #region Handle Container Class Breakpoints
        
        if (AllSegments[^1] == "container")
        {
            var hasMinBreakpoint = false;
            var hasMaxBreakpoint = false;

            WorkingSb ??= AppRunner.StringBuilderPool.Get();
            WorkingSb.Clear();

            for (var i = 0; i < AllSegments.Count; i++)
            {
                if (hasMinBreakpoint == false && AppRunner.AppRunnerSettings.BreakpointSizes.ContainsKey(AllSegments[i]))
                {
                    hasMinBreakpoint = true;
                    continue;
                }

                if (hasMaxBreakpoint == false && AllSegments[i].StartsWith("max-", StringComparison.Ordinal) && AppRunner.AppRunnerSettings.BreakpointSizes.ContainsKey(AllSegments[i].TrimStart("max-") ?? string.Empty))
                    hasMaxBreakpoint = true;
            }
            
            foreach (var bp in AppRunner.AppRunnerSettings.BreakpointSizes.OrderBy(b => b.Value))
            {
                if (hasMaxBreakpoint && AllSegments.Contains($"max-{bp.Key}"))
                    break;
                
                if (hasMinBreakpoint == false || (hasMinBreakpoint && (WorkingSb.Length > 0 || AllSegments.Contains(bp.Key))))
                    WorkingSb.Append($"@variant {bp.Key} {{ max-width: var(--breakpoint-{bp.Key}); }}{Environment.NewLine}");
            }

            WorkingSb.Insert(0, $"width: 100%;{Environment.NewLine}");

            Styles = WorkingSb.ToString().Trim();
        }
        
        #endregion
        
        if (HasModifierValue)
            Styles = Styles.Replace("{1}", ModifierValue, StringComparison.Ordinal);

        if (IsImportant)
        {
            if (Styles.Contains("--", StringComparison.Ordinal) == false)
            {
                Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);
            }
            else
            {
                var splits = Styles.Split(';', StringSplitOptions.RemoveEmptyEntries);

                Styles = string.Empty;
                    
                foreach (var split in splits)
                {
                    if (split.Trim().StartsWith("--", StringComparison.Ordinal))
                        Styles += split.Trim() + ';' + AppRunner.AppRunnerSettings.LineBreak;
                    else
                        Styles += split.Trim() + " !important;" + AppRunner.AppRunnerSettings.LineBreak;
                }
                    
                Styles = Styles.Trim();
            }
        }

        Styles = Styles.Replace("calc(var(--spacing) * 0)", "0", StringComparison.Ordinal);

        if (Value == "1")
            Styles = Styles.Replace("calc(var(--spacing) * 1)", "var(--spacing)", StringComparison.Ordinal);
    }

    #endregion
}
