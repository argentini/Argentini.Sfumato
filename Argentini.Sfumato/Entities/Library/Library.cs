// ReSharper disable RawStringCanBeSimplified
// ReSharper disable MemberCanBePrivate.Global

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Argentini.Sfumato.Entities.Library;

public sealed class Library
{
    #region Theme Properties

    public Dictionary<string, string> ColorsByName { get; set; } = [];

    public HashSet<string> CssLengthUnits { get; } = LibraryUnits.CssLengthUnits.ToHashSet(StringComparer.Ordinal);
    
    public HashSet<string> CssAngleUnits { get; } = LibraryUnits.CssAngleUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssDurationUnits { get; } = LibraryUnits.CssDurationUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssFrequencyUnits { get; } = LibraryUnits.CssFrequencyUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> CssResolutionUnits { get; } = LibraryUnits.CssResolutionUnits.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidFileExtensions { get; } = LibraryValidFileExtensions.ValidFileExtensions.ToHashSet(StringComparer.Ordinal);
    public HashSet<string> InvalidFileExtensions { get; } = LibraryValidFileExtensions.InvalidFileExtensions.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidSafariCssPropertyNames { get; } = LibraryCssPropertyNames.ValidSafariCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    public HashSet<string> ValidChromeCssPropertyNames { get; } = LibraryCssPropertyNames.ValidChromeCssPropertyNames.ToHashSet(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> MediaQueryPrefixes { get; } = LibraryMediaQueries.MediaQueryPrefixes.ToDictionary(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> SupportsQueryPrefixes { get; } = LibrarySupportsQueries.SupportsQueryPrefixes.ToDictionary(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> ContainerQueryPrefixes { get; } = LibraryContainerQueries.ContainerQueryPrefixes.ToDictionary(StringComparer.Ordinal);

    public Dictionary<string, VariantMetadata> PseudoclassPrefixes { get; } = LibraryPseudoClasses.PseudoclassPrefixes.ToDictionary(StringComparer.Ordinal);

    public readonly string[] ColorSpaces = ["srgb-linear", "display-p3", "a98-rgb", "prophoto-rgb", "rec2020", "oklab", "xyz-d50", "xyz-d65", "xyz", "hsl", "hwb", "lch", "lab"];
    
    #endregion
    
    #region Runtime Properties

    public static int FileAccessRetryMs => 5000;
    public static int MaxConsoleWidth => GetMaxConsoleWidth();

    private static int GetMaxConsoleWidth()
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

    #endregion
    
    #region Scanner Collections
    
    public PrefixTrie CssPropertyNamesWithColons { get; set; } = new();
    public PrefixTrie ScannerClassNamePrefixes { get; set; } = new();

    public Dictionary<string, ClassDefinition> SimpleClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> AbstractClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> AngleHueClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> ColorClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> DurationClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> FlexClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> FloatNumberClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> FrequencyClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> IntegerClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> LengthClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> PercentageClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> RatioClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> ResolutionClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> StringClasses { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string, ClassDefinition> UrlClasses { get; set; } = new(StringComparer.Ordinal);

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

                ScannerClassNamePrefixes.Insert(item.Key);
            }
        }        
    }

    // ReSharper disable UnusedAutoPropertyAccessor.Local
    // ReSharper disable CollectionNeverQueried.Local
    private sealed class ExportItem
    {
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<string,ClassDefinition> Usages { get; } = [];
    }
    
    public string ExportDefinitions(AppRunner appRunner)
    {
        var exportItems = new List<ExportItem>();
        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in derivedTypes.OrderBy(t => t.AssemblyQualifiedName))
        {
            if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
                continue;

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
            
            instance.ProcessThemeSettings(appRunner);
            
            var segments = type.FullName?.Split('.') ?? [];

            if (segments.Length < 2)
                continue;

            var exportItem = new ExportItem
            {
                Category = segments[^2].PascalCaseToSpaced(),
                Name = segments[^1].PascalCaseToSpaced(),
                Description = instance.Description ?? string.Empty,
            };
            
            foreach (var item in instance.Data)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in SimpleClasses)
                exportItem.Usages.Add(item.Key, item.Value);
            
            foreach (var item in AbstractClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in AngleHueClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in ColorClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in DurationClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in FlexClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in FloatNumberClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in FrequencyClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in IntegerClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in LengthClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in PercentageClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in RatioClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in ResolutionClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in StringClasses)
                exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in UrlClasses)
                exportItem.Usages.Add(item.Key, item.Value);
            
            exportItems.Add(exportItem);
        }

        var json = JsonSerializer.Serialize(exportItems, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            IncludeFields = true,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        return json;
    }
}