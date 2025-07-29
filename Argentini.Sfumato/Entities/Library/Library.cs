// ReSharper disable RawStringCanBeSimplified
// ReSharper disable MemberCanBePrivate.Global

using System.Reflection;

namespace Argentini.Sfumato.Entities.Library;

public sealed class Library
{
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

        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in derivedTypes)
        {
            if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
                continue;

            foreach (var item in instance.Data.Where(item => item.Key.EndsWith('(') == false && item.Key.EndsWith('[') == false))
            {
                if (item.Value.InAbstractValueCollection)
                    AbstractClasses.Add(item.Key, item.Value);

                if (item.Value.InSimpleUtilityCollection)
                    SimpleClasses.Add(item.Key, item.Value);
                
                if (item.Value.InFloatNumberCollection)
                    FloatNumberClasses.Add(item.Key, item.Value);
                
                if (item.Value.InAngleHueCollection)
                    AngleHueClasses.Add(item.Key, item.Value);
                
                if (item.Value.InColorCollection)
                    ColorClasses.Add(item.Key, item.Value);
                
                if (item.Value.InLengthCollection)
                    LengthClasses.Add(item.Key, item.Value);
                
                if (item.Value.InDurationCollection)
                    DurationClasses.Add(item.Key, item.Value);
                
                if (item.Value.InFlexCollection)
                    FlexClasses.Add(item.Key, item.Value);
                
                if (item.Value.InFrequencyCollection)
                    FrequencyClasses.Add(item.Key, item.Value);
                
                if (item.Value.InUrlCollection)
                    UrlClasses.Add(item.Key, item.Value);
                
                if (item.Value.InIntegerCollection)
                    IntegerClasses.Add(item.Key, item.Value);

                if (item.Value.InPercentageCollection)
                    PercentageClasses.Add(item.Key, item.Value);

                if (item.Value.InRatioCollection)
                    RatioClasses.Add(item.Key, item.Value);
                
                if (item.Value.InResolutionCollection)
                    ResolutionClasses.Add(item.Key, item.Value);
                
                if (item.Value.InStringCollection)
                    StringClasses.Add(item.Key, item.Value);

                ScannerClassNamePrefixes.Insert(item.Key, null);
            }
        }        
    }
}