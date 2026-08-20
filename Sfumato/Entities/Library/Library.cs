// ReSharper disable RawStringCanBeSimplified
// ReSharper disable MemberCanBePrivate.Global

using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Trie;
using Sfumato.Entities.UtilityClasses;

namespace Sfumato.Entities.Library;

public sealed class Library
{
    private static readonly UtilityClassRegistry BaseRegistry = UtilityClassRegistry.Instance;

    internal static IReadOnlyList<ClassDictionaryBase> ThemeHandlers => BaseRegistry.ThemeHandlers;

    #region Theme Properties

    public PrefixTrie<string> ColorsByName { get; set; } = new();

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
    public PrefixTrie<object?> ScannerClassNamePrefixes { get; set; } = new(BaseRegistry.ScannerClassNamePrefixes);

    public PrefixTrie<ClassDefinition> SimpleClasses { get; set; } = new(BaseRegistry.SimpleClasses);
    public PrefixTrie<ClassDefinition> AbstractClasses { get; set; } = new(BaseRegistry.AbstractClasses);
    public PrefixTrie<ClassDefinition> AngleHueClasses { get; set; } = new(BaseRegistry.AngleHueClasses);
    public PrefixTrie<ClassDefinition> ColorClasses { get; set; } = new(BaseRegistry.ColorClasses);
    public PrefixTrie<ClassDefinition> DurationClasses { get; set; } = new(BaseRegistry.DurationClasses);
    public PrefixTrie<ClassDefinition> FlexClasses { get; set; } = new(BaseRegistry.FlexClasses);
    public PrefixTrie<ClassDefinition> FloatNumberClasses { get; set; } = new(BaseRegistry.FloatNumberClasses);
    public PrefixTrie<ClassDefinition> FrequencyClasses { get; set; } = new(BaseRegistry.FrequencyClasses);
    public PrefixTrie<ClassDefinition> IntegerClasses { get; set; } = new(BaseRegistry.IntegerClasses);
    public PrefixTrie<ClassDefinition> LengthClasses { get; set; } = new(BaseRegistry.LengthClasses);
    public PrefixTrie<ClassDefinition> PercentageClasses { get; set; } = new(BaseRegistry.PercentageClasses);
    public PrefixTrie<ClassDefinition> RatioClasses { get; set; } = new(BaseRegistry.RatioClasses);
    public PrefixTrie<ClassDefinition> ResolutionClasses { get; set; } = new(BaseRegistry.ResolutionClasses);
    public PrefixTrie<ClassDefinition> StringClasses { get; set; } = new(BaseRegistry.StringClasses);
    public PrefixTrie<ClassDefinition> UrlClasses { get; set; } = new(BaseRegistry.UrlClasses);

    #endregion
    
    public Library()
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
            if (pseudoClass.Key.StartsWith('*'))
                continue;

            PseudoclassPrefixes.Add($"not-{pseudoClass.Key}", pseudoClass.Value.CreateNewVariant(pseudoClass.Value.PrefixType, suffix: $":not({pseudoClass.Value.SelectorSuffix})"));
        }
        
        foreach (var propertyName in ValidSafariCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:", null);
        
        foreach (var propertyName in ValidChromeCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:", null);

    }
}
