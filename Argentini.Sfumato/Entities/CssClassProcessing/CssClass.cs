// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.CssClassProcessing;

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
    public HashSet<string> AllSegments { get; } = [];

    /// <summary>
    /// Variant segments used in the class name.
    /// (e.g. "dark:tabp:[&.active]:text-base/6" => ["dark", "tabp", "[&.active]"])
    /// </summary>
    public Dictionary<string,VariantMetadata> VariantSegments { get; } = new(StringComparer.Ordinal);

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition;

    /// <summary>
    /// Ordered list of nested wrapper statements (for variants)
    /// </summary>
    public Dictionary<ulong, string> Wrappers { get; } = [];

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

    private StringBuilder? Sb { get; set; }

    #endregion
    
    #region Lifecycle

    public CssClass(AppRunner appRunner, string selector)
    {
        AppRunner = appRunner;
        Selector = selector;

        Initialize();
    }

    public void Dispose()
    {
        if (Sb is not null)
            AppRunner.AppState.StringBuilderPool.Return(Sb);
    }

    #endregion

    #region Initialization

    private void Initialize()
    {
        IsImportant = Selector.EndsWith('!');

        foreach (var segment in Selector.TrimEnd('!').SplitByTopLevel(':'))
            AllSegments.Add(segment.ToString());

        ProcessArbitraryCss();
        
        if (IsValid == false)
            ProcessUtilityClasses();

        if (IsValid == false)
            return;

        ProcessVariants();

        if (IsValid)
        {
            Sb = AppRunner.AppState.StringBuilderPool.Get();
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

                if (segment.TryVariantIsMediaQuery(AppRunner, out var mediaQuery))
                {
                    if (mediaQuery is null)
                        return;

                    VariantSegments.Add(segment, mediaQuery);
                }
                else if (segment.TryVariantIsContainerQuery(AppRunner, out var containerQuery))
                {
                    if (containerQuery is null)
                        return;

                    VariantSegments.Add(segment, containerQuery);
                }
                else if (segment.TryVariantIsPseudoClass(AppRunner, out var pseudoClass))
                {
                    if (pseudoClass is null)
                        return;

                    VariantSegments.Add(segment, pseudoClass);
                    
                    if (pseudoClass.SelectorSuffix.Contains(":where(", StringComparison.Ordinal) || pseudoClass.SelectorSuffix.Contains(":is(", StringComparison.Ordinal))
                        SelectorSort = 99;
                    else
                        SelectorSort++;
                }
                else if (segment.TryVariantIsGroup(AppRunner, out var group))
                {
                    if (group is null)
                        return;

                    VariantSegments.Add(segment, group);
                }
                else if (segment.TryVariantIsPeer(AppRunner, out var peer))
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
                else if (segment.TryVariantIsHas(AppRunner, out var has))
                {
                    if (has is null)
                        return;

                    VariantSegments.Add(segment, has);
                }
                else if (segment.TryVariantIsSupports(AppRunner, out var supports))
                {
                    if (supports is null)
                        return;

                    VariantSegments.Add(segment, supports);
                }
                else if (segment.TryVariantIsNotSupports(AppRunner, out var notSupports))
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
                else if (segment.TryVariantIsCustom(AppRunner, out var custom))
                {
                    if (custom is null)
                        return;

                    VariantSegments.Add(segment, custom);
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
    
    private void ProcessArbitraryCss()
    {
        try
        {
            if (AllSegments.Last().StartsWith('[') == false || AllSegments.Last().EndsWith(']') == false)
                return;
            
            var trimmedValue = AllSegments.Last().TrimStart('[').TrimEnd(']').Trim('_');
            var colonIndex = trimmedValue.IndexOf(':');

            if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                return;

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
    
    private void ProcessUtilityClasses()
    {
        try
        {
            var prefix = AppRunner.Library.ScannerClassNamePrefixes.GetLongestMatchingPrefix(AllSegments.Last().Contains('/') ? AllSegments.Last()[..AllSegments.Last().IndexOf('/')] : AllSegments.Last());

            if (string.IsNullOrEmpty(prefix))
                return;

            var value = AllSegments.Last().TrimStart(prefix) ?? string.Empty;

            if (value.Contains('/'))
            {
                var slashSegments = new List<string>();
                
                foreach (var segment in value.SplitByTopLevel('/'))
                    slashSegments.Add(segment.ToString());

                if (slashSegments.Count == 2)
                {
                    ModifierValue = slashSegments.Last();
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
                if (AppRunner.Library.SimpleClasses.TryGetValue(prefix, out ClassDefinition))
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
                    if (AppRunner.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition))
                    {
                        if (ClassDefinition.UsesSlashModifier == false)
                            Value = $"{numerator} / {denominator}";
                    }
                    else if (AppRunner.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition) || AppRunner.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition))
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
                    AppRunner.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("color:", StringComparison.Ordinal))
                {
                    AppRunner.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("integer:", StringComparison.Ordinal))
                {
                    AppRunner.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("percentage:", StringComparison.Ordinal))
                {
                    AppRunner.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("alpha:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("number:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FloatNumberClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("image:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("url:", StringComparison.Ordinal))
                {
                    AppRunner.Library.UrlClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("angle:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("hue:", StringComparison.Ordinal))
                {
                    AppRunner.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("duration:", StringComparison.Ordinal) || valueNoBrackets.StartsWith("time:", StringComparison.Ordinal))
                {
                    AppRunner.Library.DurationClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("flex:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("frequency:", StringComparison.Ordinal))
                {
                    AppRunner.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("ratio:", StringComparison.Ordinal))
                {
                    AppRunner.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("resolution:", StringComparison.Ordinal))
                {
                    AppRunner.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("size:", StringComparison.Ordinal))
                {
                    AppRunner.Library.AbstractClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else if (valueNoBrackets.StartsWith("string:", StringComparison.Ordinal))
                {
                    AppRunner.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
                }
                else
                {
                    AppRunner.Library.AbstractClasses.TryGetValue(prefix, out ClassDefinition);
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
                    AppRunner.Library.AbstractClasses,
                };

                foreach (var dict in classDictionaries)
                {
                    if (dict.TryGetValue(prefix, out ClassDefinition))
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
                    AppRunner.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsDimensionLength(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && valueNoBrackets.ValueIsFloatNumber())
                {
                    if (AppRunner.Library.FloatNumberClasses.TryGetValue(prefix, out ClassDefinition) == false)
                        AppRunner.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && valueNoBrackets.IsValidWebColor())
                {
                    AppRunner.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsAngleHue(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsDurationTime(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.DurationClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && (valueNoBrackets.ValueIsFrequency(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null && valueNoBrackets.ValueIsUrl())
                {
                    AppRunner.Library.UrlClasses.TryGetValue(prefix, out ClassDefinition);

                    if (ClassDefinition is null)
                        AppRunner.Library.AbstractClasses.TryGetValue(prefix, out ClassDefinition);
                    else if (valueNoBrackets.StartsWith("url(", StringComparison.Ordinal) == false)
                        valueNoBrackets = $"url({valueNoBrackets})";
                }

                if (ClassDefinition is null && valueNoBrackets.ValueIsRatio())
                {
                    AppRunner.Library.RatioClasses.TryGetValue(prefix, out ClassDefinition);
                }
                
                if (ClassDefinition is null && (valueNoBrackets.ValueIsResolution(AppRunner) || valueNoBrackets.StartsWith("calc(", StringComparison.Ordinal)))
                {
                    AppRunner.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
                }

                if (ClassDefinition is null)
                {
                    if (AppRunner.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition) == false)
                    {
                        if (AppRunner.Library.AbstractClasses.TryGetValue(prefix, out ClassDefinition) == false)
                            AppRunner.Library.StringClasses.TryGetValue(prefix, out ClassDefinition);
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
                AppRunner.Library.LengthClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.IntegerClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FloatNumberClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FlexClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.AngleHueClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.DurationClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.FrequencyClasses.TryGetValue(prefix, out ClassDefinition);

                if (ClassDefinition is null)
                    AppRunner.Library.ResolutionClasses.TryGetValue(prefix, out ClassDefinition);
            }

            if (ClassDefinition is null && value.ValueIsPercentage())
                AppRunner.Library.PercentageClasses.TryGetValue(prefix, out ClassDefinition);

            if (ClassDefinition is not null)
                Value = value;

            if (ClassDefinition is null && value.ValueIsColorName(AppRunner))
            {
                if (AppRunner.Library.ColorClasses.TryGetValue(prefix, out ClassDefinition))
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
   
    private void GenerateSelector()
    {
        Sb?.Clear();
        
        try
        {
            var hasDescendantVariant = VariantSegments
                .Any(s => s.Value.PrefixType == "pseudoclass" && s.Key is "*" or "**");
            
            if (hasDescendantVariant)
                Sb?.Append(":is(");
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "prefix").OrderByDescending(s => s.Value.PrefixOrder))
                Sb?.Append(variant.Value.SelectorPrefix);

            Sb?.Append('.');
            Sb?.Append(Selector.CssSelectorEscape());
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "pseudoclass").OrderByDescending(s => s.Value.PrefixOrder))
            {
                Sb?.Append(variant.Key.StartsWith("data-", StringComparison.OrdinalIgnoreCase) ? $"[{variant.Key}]" : variant.Value.SelectorSuffix);
            }

            if (hasDescendantVariant)
            {
                if (VariantSegments.Any(s => s.Value.PrefixType == "pseudoclass" && s.Key == "*"))
                    Sb?.Append(" > *)");
                else if (VariantSegments.Any(s => s.Value.PrefixType == "pseudoclass" && s.Key == "**"))
                    Sb?.Append(" *)");
            }

            EscapedSelector = Sb?.ToString() ?? string.Empty;
        }
        catch
        {
            IsValid = false;
        }
    }

    private void GenerateWrappers()
    {
        if (Sb is null)
            return;

        Sb.Clear();

        try
        {
            if (VariantSegments.TryGetValue("dark", out var darkVariant) && darkVariant.PrefixType == "media")
            {
                var wrapper = $"@{darkVariant.PrefixType} {darkVariant.Statement} {{";
                
                Wrappers.Add(wrapper.Fnv1AHash64(), wrapper);
                
                UsesDarkTheme = true;
                WrapperSort += darkVariant.PrefixOrder;
            }

            foreach (var queryType in new[] { "media", "supports" })
            {
                foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == queryType && s.Key != "dark").OrderBy(s => s.Value.PrefixOrder))
                {
                    if (Sb.Length == 0)
                    {
                        Sb.Append($"@{queryType} ");
                        
                        if (queryType == "media" && WrapperSort <= int.MaxValue - variant.Value.PrefixOrder)
                            WrapperSort += variant.Value.PrefixOrder;
                    }
                    else
                    {
                        Sb.Append(" and ");

                        if (queryType == "media" && WrapperSort < int.MaxValue)
                            WrapperSort += 1;
                    }

                    Sb.Append(variant.Value.Statement);
                }

                if (Sb.Length <= 0)
                    continue;
                
                Sb.Append(" {");
                Wrappers.Add(Sb.Fnv1AHash64(), Sb.ToString());
                Sb.Clear();
            }
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType == "container").OrderBy(s => s.Value.PrefixOrder))
            {
                var indexOfSlash = variant.Key.LastIndexOf('/');
                var modifierValue = string.Empty;

                if (indexOfSlash > 0)
                    modifierValue = variant.Key[(indexOfSlash + 1)..];

                if (Sb.Length == 0)
                    Sb.Append($"@container {(string.IsNullOrEmpty(modifierValue) ? string.Empty : $"{modifierValue} ")}");
                else
                    Sb.Append(" and ");
            
                Sb.Append(variant.Value.Statement);
            }

            if (Sb.Length > 0)
            {
                Sb.Append(" {");
                Wrappers.Add(Sb.Fnv1AHash64(), Sb.ToString());
            }
            
            foreach (var variant in VariantSegments.Where(s => s.Value.PrefixType is "wrapper").OrderBy(s => s.Value.PrefixOrder))
            {
                var wrapper = $"{variant.Value.Statement} {{";
                
                Wrappers.Add(wrapper.Fnv1AHash64(), wrapper);
            }
        }
        catch
        {
            IsValid = false;
        }
    }

    private void GenerateStyles(bool useArbitraryValue = false)
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

        if (HasModifierValue)
            Styles = Styles.Replace("{1}", ModifierValue, StringComparison.Ordinal);
            
        if (IsImportant)
            Styles = Styles.Replace(";", " !important;", StringComparison.Ordinal);

        Styles = Styles.Replace("calc(var(--spacing) * 0)", "0", StringComparison.Ordinal);
    }

    #endregion
}