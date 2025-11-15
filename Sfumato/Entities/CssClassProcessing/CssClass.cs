// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantBoolCompare
// ReSharper disable ForCanBeConvertedToForeach

using Sfumato.Entities.Scanning;
using Sfumato.Helpers;
using Sfumato.Validators;
using Sfumato.Entities.Runners;
using Sfumato.Entities.Trie;
using Sfumato.Entities.UtilityClasses;

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
        IsImportant = Selector[^1] == '!';

        if (Selector.IndexOf(':') > 0)
        {
            foreach (var segment in Selector.SplitByTopLevel(':'))
                AllSegments.Add(segment.ToString());

            return false;
        }

        var hasBrackets = Selector.IndexOfAny(['[', '(']) >= 0;

        AllSegments.Add(IsImportant ? Selector[..^1] : Selector);

        if (hasBrackets == false && AppRunner.Library.SimpleClasses.TryGetValue(AllSegments[0], out ClassDefinition))
        {
            IsValid = true;
            SelectorSort = ClassDefinition.SelectorSort;

            if (ClassDefinition.IsRazorSyntax)
                HasRazorSyntax = true;
            
            GenerateSelector();
            GenerateStyles();

            return true;
        }

        return false;
    }
    
    public void Initialize()
    {
        if (ProcessSelectorSegments())
            return; // Exit early if a simple class with no variants is found
        
        ProcessArbitraryCss();

        if (IsValid == false && AllSegments[^1][0] != '[')
            ProcessUtilityClasses();

        if (IsValid == false)
            return;

        if (ClassDefinition?.IsRazorSyntax ?? false)
            HasRazorSyntax = true;

        if (AllSegments.Count > 1)
            ProcessVariants();

        if (IsValid)
            GenerateSelector();

        if (IsValid)
            GenerateWrappers();
    }
    
    #endregion
    
    #region Processing
    
    public void ProcessVariants()
    {
        try
        {
            if (AllSegments.Count <= 1)
                return;
            
            // One or more invalid variants invalidate the entire utility class

            for (var i = 0; i < AllSegments.Count - 1; i++) // skip last item
            {
                var segment = AllSegments[i];
                
                if (string.IsNullOrEmpty(segment))
                    return;

                if (segment.TryGetVariant(AppRunner, out var variantMetadata))
                {
                    if (variantMetadata is null)
                        return;

                    if (HasRazorSyntax == false && variantMetadata.IsRazorSyntax)
                        HasRazorSyntax = true;
                    
                    VariantSegments.TryAdd(segment, variantMetadata);

                    if (variantMetadata.PrefixType[0] != 'p')
                        continue;
                    
                    if (variantMetadata.PrioritySort > 0)
                        SelectorSort += variantMetadata.PrioritySort;
                    else
                        SelectorSort++;
                }
                else
                {
                    IsValid = false;
                    return;
                }
            }
        }
        catch
        {
            // Ignored
        }
    }
    
    public void ProcessArbitraryCss()
    {
        try
        {
            if (AllSegments.Count == 0)
                return;

            if (AllSegments.Last().StartsWith('[') == false || AllSegments.Last().EndsWith(']') == false)
                return;
            
            var trimmedValue = AllSegments.Last().TrimStart('[').TrimEnd(']').Trim('_');
            var colonIndex = trimmedValue.IndexOf(':');

            if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                return;

            Styles = string.Empty;
            
            foreach (var span in trimmedValue.EnumerateCssCustomProperties())
            {
                // [--my-text-size:1rem]
                IsCssCustomPropertyAssignment = true;
                IsValid = true;
                Styles += $"{span.Property}: {span.Value};".Replace('_', ' ');
            }
            
            if (IsValid == false && AppRunner.Library.CssPropertyNamesWithColons.HasPrefixIn(trimmedValue))
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
    
    public void ProcessUtilityClasses()
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

            if (value.Contains('/'))
            {
                var slashSegments = new List<string>();
                
                foreach (var segment in value.SplitByTopLevel('/'))
                    slashSegments.Add(segment.ToString());

                if (slashSegments.Count == 2)
                {
                    ModifierValue = slashSegments[^1];
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
            
            if (HasArbitraryValue == false && HasArbitraryValueWithCssCustomProperty == false && HasModifierValue && int.TryParse(value, out var numerator) && int.TryParse(ModifierValue, out var denominator))
            {
                if (denominator != 0)
                {
                    if (AppRunner.Library.RatioClasses.TryGetValue(_prefix, out ClassDefinition))
                    {
                        if (ClassDefinition.UsesSlashModifier == false)
                            Value = $"{numerator} / {denominator}";
                    }
                    else if (AppRunner.Library.LengthClasses.TryGetValue(_prefix, out ClassDefinition) || AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition))
                    {
                        if (ClassDefinition.UsesSlashModifier == false)
                            Value = $"{(double)numerator / denominator * 100:0.############}%";
                    }

                    if (ClassDefinition?.UsesSlashModifier ?? false)
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
                var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(')').TrimEnd(']').Replace('_', ' ');

                if (valueNoBrackets.StartsWith("dimension:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("length:", StringComparison.Ordinal))
                {
                    AppRunner.Library.LengthClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("color:", StringComparison.Ordinal))
                {
                    AppRunner.Library.ColorClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("integer:", StringComparison.Ordinal))
                {
                    AppRunner.Library.IntegerClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("percentage:", StringComparison.Ordinal))
                {
                    AppRunner.Library.PercentageClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("alpha:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("number:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FloatNumberClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("image:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("url:", StringComparison.Ordinal))
                {
                    AppRunner.Library.UrlClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("angle:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("hue:", StringComparison.Ordinal))
                {
                    AppRunner.Library.AngleHueClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("duration:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("time:", StringComparison.Ordinal))
                {
                    AppRunner.Library.DurationClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("flex:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FlexClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("frequency:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FrequencyClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("ratio:", StringComparison.Ordinal))
                {
                    AppRunner.Library.RatioClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("resolution:", StringComparison.Ordinal))
                {
                    AppRunner.Library.ResolutionClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("size:", StringComparison.Ordinal))
                {
                    AppRunner.Library.AbstractClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("string:", StringComparison.Ordinal))
                {
                    AppRunner.Library.StringClasses.TryGetValue(_prefix, out ClassDefinition);
                }
                else
                {
                    AppRunner.Library.AbstractClasses.TryGetValue(_prefix, out ClassDefinition);
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

                var classDictionaries = new List<PrefixTrie<ClassDefinition>>
                {
                    AppRunner.Library.LengthClasses,
                    AppRunner.Library.ColorClasses,
                    AppRunner.Library.PercentageClasses,
                    AppRunner.Library.IntegerClasses,
                    AppRunner.Library.FloatNumberClasses,
                    AppRunner.Library.AngleHueClasses,
                    AppRunner.Library.DurationClasses,
                    AppRunner.Library.FrequencyClasses,
                    AppRunner.Library.UrlClasses,
                    AppRunner.Library.FlexClasses,
                    AppRunner.Library.RatioClasses,
                    AppRunner.Library.ResolutionClasses,
                    AppRunner.Library.StringClasses,
                    AppRunner.Library.AbstractClasses
                };

                foreach (var dict in classDictionaries)
                {
                    if (dict.TryGetValue(_prefix, out ClassDefinition))
                        break;
                }                

                if (ClassDefinition is not null)
                {
                    var valueNoBrackets = value.TrimStart('[').TrimStart('(').TrimEnd(')').TrimEnd(']').Replace('_', ' ');

                    Value = valueNoBrackets.StartsWith("--", StringComparison.Ordinal) ? $"var({valueNoBrackets})" : valueNoBrackets;
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
                var valueNoBrackets = value.TrimStart('[').TrimEnd(']').Replace('_', ' ');

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
                    if (ClassDefinition.InColorCollection && HasModifierValue)
                    {
                        if (int.TryParse(ModifierValue, out var alphaPct))
                            Value = valueNoBrackets.SetWebColorAlpha(alphaPct);
                        else if (double.TryParse(ModifierValue, out var alpha))
                            Value = valueNoBrackets.SetWebColorAlpha(alpha);
                        else
                            Value = valueNoBrackets;
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

                if (ClassDefinition is null)
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
                            if (double.TryParse(ModifierValue, out var pct) == false)
                                pct = 100;

                            if (pct < 0)
                                pct = 0;

                            if (pct > 100)
                                pct = 100;
                            
                            if (pct < 100)
                            {
                                if (AppRunner.AppRunnerSettings.UseCompatibilityMode)
                                {
                                    Value = colorValue.SetWebColorAlphaByPercentage(pct);
                                }
                                else
                                {
                                    if (colorValue.Contains("oklch"))
                                        Value = $"color-mix(in oklab, var(--color-{value}) {pct}%, transparent)";
                                    else if (colorValue.Contains("rgb") || colorValue.Contains('#'))
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
    }

    public void GenerateSelector()
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

    public void GenerateWrappers()
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
        var indexOfSlash = firstVariant.Key.LastIndexOf('/');

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

    public void GenerateStyles(bool useArbitraryValue = false)
    {
        if (ClassDefinition is null)
            return;

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

                if (hasMaxBreakpoint == false && AllSegments[i].StartsWith("max-") && AppRunner.AppRunnerSettings.BreakpointSizes.ContainsKey(AllSegments[i].TrimStart("max-") ?? string.Empty))
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
            Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);

        Styles = Styles.Replace("calc(var(--spacing) * 0)", "0", StringComparison.Ordinal);
    }

    #endregion
}