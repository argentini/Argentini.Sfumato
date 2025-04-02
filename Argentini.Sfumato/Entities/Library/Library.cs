// ReSharper disable RawStringCanBeSimplified
// ReSharper disable MemberCanBePrivate.Global

using System.Reflection;
using Argentini.Sfumato.Entities.CssClassProcessing;
using Argentini.Sfumato.Entities.Trie;
using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato.Entities.Library;

public sealed class Library
{
    #region Utility Class Constants

    public Dictionary<string, string> ColorsByName { get; set; } = LibraryColors.Values.ToDictionary(StringComparer.Ordinal);

    public HashSet<string> CssDataTypes { get; } = LibraryUnits.CssDataTypes.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssLengthUnits { get; } = LibraryUnits.CssLengthUnits.ToHashSet(StringComparer.Ordinal);
    
    public HashSet<string> CssAngleUnits { get; } = LibraryUnits.CssAngleUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssDurationUnits { get; } = LibraryUnits.CssDurationUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssFrequencyUnits { get; } = LibraryUnits.CssFrequencyUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssResolutionUnits { get; } = LibraryUnits.CssResolutionUnits.ToHashSet(StringComparer.Ordinal);
    
    private HashSet<string> ValidSafariCssPropertyNames { get; } = LibraryCssPropertyNames.ValidSafariCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    private HashSet<string> ValidChromeCssPropertyNames { get; } = LibraryCssPropertyNames.ValidChromeCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> MediaQueryPrefixes { get; } = LibraryMediaQueries.MediaQueryPrefixes.ToDictionary(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> ContainerQueryPrefixes { get; } = LibraryContainerQueries.ContainerQueryPrefixes.ToDictionary(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> PseudoclassPrefixes { get; } = LibraryPseudoClasses.PseudoclassPrefixes.ToDictionary(StringComparer.Ordinal);

    #endregion
    
    #region Runtime Properties

    public static int FileAccessRetryMs => 5000;
    public static int MaxConsoleWidth => GetMaxConsoleWidth();

    private static int GetMaxConsoleWidth()
    {
        try
        {
            return Console.WindowWidth > 120 ? 120 : Console.WindowWidth - 1;
        }
        catch
        {
            return 78;
        }
    }

    #endregion
    
    #region Scanner Collections
    
    public PrefixTrie CssPropertyNamesWithColons { get; set; } = new();
    public PrefixTrie ScannerClassNamePrefixes { get; set; } = new();

    public Dictionary<string, ClassDefinition> SimpleClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> SpacingClasses { get; set; } = new(StringComparer.Ordinal);
    
    public Dictionary<string, ClassDefinition> AlphaNumberClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> AngleHueClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> ColorClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> DimensionLengthClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> DurationTimeClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> FlexClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> FrequencyClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> ImageUrlClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> IntegerClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> RatioClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> ResolutionClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> StringClasses { get; set; } = new(StringComparer.Ordinal);

    public HashSet<string> UsedCssCustomProperties { get; } = [];

    #endregion
    
    public Library()
    {
        foreach (var pseudoClass in PseudoclassPrefixes.ToDictionary(StringComparer.Ordinal))
        {
            PseudoclassPrefixes.Add($"not-{pseudoClass.Key}", new VariantMetadata
            {
                PrefixType = pseudoClass.Value.PrefixType,
                Statement =$":not({pseudoClass.Value.Statement})"
            });
        }
        
        foreach (var propertyName in ValidSafariCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:");
        
        foreach (var propertyName in ValidChromeCssPropertyNames)
            CssPropertyNamesWithColons.Insert($"{propertyName}:");

        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in derivedTypes)
        {
            if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
                continue;

            foreach (var item in instance.Data.Where(item => item.Key.EndsWith('(') == false && item.Key.EndsWith('[') == false))
            {
                if (item.Value.IsSimpleUtility)
                    SimpleClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesSpacing)
                    SpacingClasses.Add(item.Key, item.Value);

                if (item.Value.UsesAlphaNumber)
                    AlphaNumberClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesAngleHue)
                    AngleHueClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesColor)
                    ColorClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesDimensionLength)
                    DimensionLengthClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesDurationTime)
                    DurationTimeClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesFlex)
                    FlexClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesFrequency)
                    FrequencyClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesImageUrl)
                    ImageUrlClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesInteger)
                    IntegerClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesRatio)
                    RatioClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesResolution)
                    ResolutionClasses.Add(item.Key, item.Value);
                
                if (item.Value.UsesString)
                    StringClasses.Add(item.Key, item.Value);

                ScannerClassNamePrefixes.Insert(item.Key);
            }
        }        
    }
}