// ReSharper disable RawStringCanBeSimplified
// ReSharper disable MemberCanBePrivate.Global

using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Trie;
using Sfumato.Entities.UtilityClasses;

namespace Sfumato.Entities.Library;

public sealed class Library
{
    #region Theme Properties

    public PrefixTrie<string> ColorsByName { get; set; } = new();
    public PrefixTrie<List<FunctionalUtilityDefinition>> FunctionalClasses { get; } = new();

    public HashSet<string> CssLengthUnits { get; } = LibraryUnits.CssLengthUnits.ToHashSet(StringComparer.Ordinal);
    
    public HashSet<string> CssAngleUnits { get; } = LibraryUnits.CssAngleUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssDurationUnits { get; } = LibraryUnits.CssDurationUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssFrequencyUnits { get; } = LibraryUnits.CssFrequencyUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssResolutionUnits { get; } = LibraryUnits.CssResolutionUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidFileExtensions { get; } = LibraryValidFileExtensions.ValidFileExtensions.ToHashSet(StringComparer.Ordinal);
    public HashSet<string> InvalidFileExtensions { get; } = LibraryValidFileExtensions.InvalidFileExtensions.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidSafariCssPropertyNames { get; } = LibraryCssPropertyNames.ValidSafariCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidChromeCssPropertyNames { get; } = LibraryCssPropertyNames.ValidChromeCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    public PrefixTrie<VariantMetadata> MediaQueryPrefixes { get; } = new();

    public PrefixTrie<VariantMetadata> SupportsQueryPrefixes { get; } = new();
    public PrefixTrie<VariantMetadata> StartingStyleQueryPrefixes { get; } = new();

    public PrefixTrie<VariantMetadata> ContainerQueryPrefixes { get; } = new();

    public PrefixTrie<VariantMetadata> PseudoclassPrefixes { get; } = new();

    public PrefixTrie<VariantMetadata> AllVariants { get; } = new();

    public readonly string[] ColorSpaces = ["srgb-linear", "display-p3", "a98-rgb", "prophoto-rgb", "rec2020", "oklab", "xyz-d50", "xyz-d65", "xyz", "hsl", "hwb", "lch", "lab"];
    
    #endregion
    
    #region Runtime Properties

    public static int FileAccessRetryMs => 5000;
    public static int MaxConsoleWidth => InternalGetMaxConsoleWidth();

    private static int InternalGetMaxConsoleWidth()
    {
        try
        {
            return Console.WindowWidth is > 120 or < 1 ? 120 : Console.WindowWidth - 1;
        }
        catch
        {
            return 78;
        }
    }

    public int GetMaxConsoleWidth()
    {
        return InternalGetMaxConsoleWidth();
    }

    #endregion
    
    #region Scanner Collections
    
    public PrefixTrie<object?> CssPropertyNamesWithColons { get; set; } = new();
    public PrefixTrie<object?> ScannerClassNamePrefixes { get; set; } = new();

    public PrefixTrie<ClassDefinition> SimpleClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> AbstractClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> AngleHueClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> ColorClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> DurationClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> FlexClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> FloatNumberClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> FrequencyClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> IntegerClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> LengthClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> PercentageClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> RatioClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> ResolutionClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> StringClasses { get; set; } = new();
    public PrefixTrie<ClassDefinition> UrlClasses { get; set; } = new();

    #endregion

    public bool TryGetClassDefinition(string prefix, UtilityValueKinds valueKind, out ClassDefinition? definition)
    {
        definition = null;

        var found = valueKind switch
        {
            UtilityValueKinds.Abstract => AbstractClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Angle => AngleHueClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Color => ColorClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Duration => DurationClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Flex => FlexClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Number => FloatNumberClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Frequency => FrequencyClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Integer => IntegerClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Length => LengthClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Percentage => PercentageClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Ratio => RatioClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Resolution => ResolutionClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.String => StringClasses.TryGetValue(prefix, out definition),
            UtilityValueKinds.Url => UrlClasses.TryGetValue(prefix, out definition),
            _ => false,
        };

        return found;
    }

    public bool TryGetUntypedCustomPropertyDefinition(string prefix, out ClassDefinition? definition)
    {
        return LengthClasses.TryGetValue(prefix, out definition)
            || ColorClasses.TryGetValue(prefix, out definition)
            || PercentageClasses.TryGetValue(prefix, out definition)
            || IntegerClasses.TryGetValue(prefix, out definition)
            || FloatNumberClasses.TryGetValue(prefix, out definition)
            || AngleHueClasses.TryGetValue(prefix, out definition)
            || DurationClasses.TryGetValue(prefix, out definition)
            || FrequencyClasses.TryGetValue(prefix, out definition)
            || UrlClasses.TryGetValue(prefix, out definition)
            || FlexClasses.TryGetValue(prefix, out definition)
            || RatioClasses.TryGetValue(prefix, out definition)
            || ResolutionClasses.TryGetValue(prefix, out definition)
            || StringClasses.TryGetValue(prefix, out definition)
            || AbstractClasses.TryGetValue(prefix, out definition);
    }
    
    public Library(bool includeBuiltInUtilities = true)
    {
        foreach (var kvp in LibraryMediaQueries.MediaQueryPrefixes)
            MediaQueryPrefixes.Add(kvp.Key, kvp.Value.CreateNewVariant());

        foreach (var kvp in LibrarySupportsQueries.SupportsQueryPrefixes)
            SupportsQueryPrefixes.Add(kvp.Key, kvp.Value.CreateNewVariant());

        foreach (var kvp in LibraryStartingStyleQueries.StartingStyleQueryPrefixes)
            StartingStyleQueryPrefixes.Add(kvp.Key, kvp.Value.CreateNewVariant());

        foreach (var kvp in LibraryContainerQueries.ContainerQueryPrefixes)
            ContainerQueryPrefixes.Add(kvp.Key, kvp.Value.CreateNewVariant());

        foreach (var kvp in LibraryPseudoClasses.PseudoclassPrefixes)
            PseudoclassPrefixes.Add(kvp.Key, kvp.Value.CreateNewVariant());

        foreach (var pseudoClass in PseudoclassPrefixes.ToDictionary(StringComparer.Ordinal))
        {
            // Skip pseudo-elements (*, **) and the arbitrary not- prefix itself so we
            // don't generate bogus variants like not-not-*.
            if (pseudoClass.Key.StartsWith('*') || pseudoClass.Key.StartsWith("not-", StringComparison.Ordinal))
                continue;

            PseudoclassPrefixes.Add($"not-{pseudoClass.Key}", pseudoClass.Value.CreateNewVariant(pseudoClass.Value.PrefixType, suffix: $":not({pseudoClass.Value.SelectorSuffix})"));
        }
        
        foreach (var propertyName in ValidSafariCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:", null);
        
        foreach (var propertyName in ValidChromeCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:", null);

        if (includeBuiltInUtilities == false)
            return;

        foreach (var instance in BuiltInUtilityRegistry.All)
        {
            foreach (var item in instance.Data)
            {
                if (item.Key.EndsWith('(') || item.Key.EndsWith('['))
                    continue;

                var valueKinds = item.Value.ValueKinds;

                if ((valueKinds & UtilityValueKinds.Abstract) != 0)
                    AbstractClasses.Add(item.Key, item.Value);

                if ((valueKinds & UtilityValueKinds.Simple) != 0)
                    SimpleClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Number) != 0)
                    FloatNumberClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Angle) != 0)
                    AngleHueClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Color) != 0)
                    ColorClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Length) != 0)
                    LengthClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Duration) != 0)
                    DurationClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Flex) != 0)
                    FlexClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Frequency) != 0)
                    FrequencyClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Url) != 0)
                    UrlClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Integer) != 0)
                    IntegerClasses.Add(item.Key, item.Value);

                if ((valueKinds & UtilityValueKinds.Percentage) != 0)
                    PercentageClasses.Add(item.Key, item.Value);

                if ((valueKinds & UtilityValueKinds.Ratio) != 0)
                    RatioClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.Resolution) != 0)
                    ResolutionClasses.Add(item.Key, item.Value);
                
                if ((valueKinds & UtilityValueKinds.String) != 0)
                    StringClasses.Add(item.Key, item.Value);

                ScannerClassNamePrefixes.Insert(item.Key, null);
            }
        }
    }

    public void ClearUtilityDefinitions()
    {
        ScannerClassNamePrefixes.Clear();
        SimpleClasses.Clear();
        AbstractClasses.Clear();
        AngleHueClasses.Clear();
        ColorClasses.Clear();
        DurationClasses.Clear();
        FlexClasses.Clear();
        FloatNumberClasses.Clear();
        FrequencyClasses.Clear();
        IntegerClasses.Clear();
        LengthClasses.Clear();
        PercentageClasses.Clear();
        RatioClasses.Clear();
        ResolutionClasses.Clear();
        StringClasses.Clear();
        UrlClasses.Clear();
    }
}
