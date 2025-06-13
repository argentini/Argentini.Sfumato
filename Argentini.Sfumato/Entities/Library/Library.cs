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

    public string ExportUtilityClassDefinitions(AppRunner appRunner)
    {
        var exportItems = new List<ExportItem>();
        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
            .OrderBy(t => t.AssemblyQualifiedName)
            .ToList();
        var groups = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var type in derivedTypes)
        {
            if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
                continue;

            if (string.IsNullOrEmpty(instance.GroupDescription) == false)
                groups.TryAdd(instance.Group, instance.GroupDescription);
        }

        foreach (var type in derivedTypes)
        {
            if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
                continue;

            groups.TryAdd(instance.Group, instance.Description);
        }

        foreach (var type in derivedTypes)
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
                Group = instance.Group ?? string.Empty,
                GroupDescription = groups[instance.Group ?? string.Empty],
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
            
            #region Iterate usages and add doc definitions and examples

            foreach (var usage in exportItem.Usages)
            {
                if (usage.Key.EndsWith('-'))
                {
                    if (usage.Value.InAngleHueCollection)
                    {
                        if(usage.Value.DocDefinitions.TryAdd($"{usage.Key}<angle>", usage.Value.Template.Replace("{0}", "<angle>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}45", usage.Value.Template.Replace("{0}", "45"));

                        if (usage.Value.UsesSlashModifier)
                        {
                            if(usage.Value.DocDefinitions.TryAdd($"{usage.Key}<angle>/<modifier>", usage.Value.ModifierTemplate.Replace("{0}", "<angle>").Replace("{1}", "<modifier>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}45/srgb", usage.Value.ModifierTemplate.Replace("{0}", "45").Replace("{1}", "srgb"));
                        }
                        
                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[45deg]", GetArbitraryTemplate(usage.Value).Replace("{0}", "45deg"));
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-angle)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-angle)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(angle:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(angle:--my-angle)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-angle)"));
                        }
                    }

                    if (usage.Value.InColorCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<color-name>", usage.Value.Template.Replace("{0}", "<value>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}red-500", usage.Value.Template.Replace("{0}", "var(--color-red-500)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<color-name>/<opacity>", usage.Value.Template.Replace("{0}", "<value>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}red-500/42", usage.Value.Template.Replace("{0}", "color-mix(in oklab, var(--color-red-500) 42%, transparent)"));
                        
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", usage.Value.Template.Replace("{0}", "<value>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[#ff0000]", usage.Value.Template.Replace("{0}", "#ff0000"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]/<opacity>", usage.Value.Template.Replace("{0}", "<value>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[#ff0000]/42", usage.Value.Template.Replace("{0}", "rgba(255,0,0,0.42)"));
                        
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", usage.Value.Template.Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-color)", usage.Value.Template.Replace("{0}", "var(--my-color)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(color:<custom-property>)", usage.Value.Template.Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(color:--my-color)", usage.Value.Template.Replace("{0}", "var(--my-color)"));
                    }

                    if (usage.Value.InDurationCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}250", usage.Value.Template.Replace("{0}", "250"));

                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[250ms]", GetArbitraryTemplate(usage.Value).Replace("{0}", "250ms"));
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-duration)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-duration)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(duration:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(duration:--my-duration)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-duration)"));
                        }
                    }

                    if (usage.Value.InFlexCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}1", usage.Value.Template.Replace("{0}", "1"));

                        if (usage.Value is { InFloatNumberCollection: false, InIntegerCollection: false, InAbstractValueCollection: true })
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[3_1_auto]", GetArbitraryTemplate(usage.Value).Replace("{0}", "3 1 auto"));
                        }
                        else
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[1]", GetArbitraryTemplate(usage.Value).Replace("{0}", "1"));
                        }

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-flex)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-flex)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(flex:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(flex:--my-flex)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-flex)"));
                    }

                    if (usage.Value is { InFloatNumberCollection: true, InFlexCollection: false })
                    {
                        if (usage.Value.Template.Contains("{0}%"))
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}42", usage.Value.Template.Replace("{0}", "42"));

                            if (usage.Value.InLengthCollection)
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", usage.Value.Template.Replace("{0}", "<percentage>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", usage.Value.Template.Replace("{0}", "50%"));
                            }

                            if (usage.Key.StartsWith('-') == false)
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[42%]", GetArbitraryTemplate(usage.Value).Replace("{0}", "42%"));
                            
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-percentage)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-percentage)"));

                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(percentage:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}(percentage:--my-percentage)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-percentage)"));
                            }
                        }
                        else if (usage.Value.Template.Contains("* {0}") || usage.Value.Template.Contains("* -{0}") || usage.Value.Template.Contains("{0}px"))
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}4", usage.Value.Template.Replace("{0}", "4"));

                            if (usage.Key.StartsWith('-') == false)
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[1rem]", GetArbitraryTemplate(usage.Value).Replace("{0}", "1rem"));
                            
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));

                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(length:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}(length:--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));
                            }
                        }
                    }

                    if (usage.Value is { InIntegerCollection: true, InFloatNumberCollection: false, InFlexCollection: false })
                    {
                        var numValue = usage.Key switch
                        {
                            "z-" or "-z-" => "99",
                            "font-" => "400",
                            _ => "2"
                        };

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}{numValue}", usage.Value.Template.Replace("{0}", numValue));

                        if (usage.Value.InLengthCollection)
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", usage.Value.Template.Replace("{0}", "<percentage>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", usage.Value.Template.Replace("{0}", "50%"));
                        }

                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.InAbstractValueCollection)
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[initial]", GetArbitraryTemplate(usage.Value).Replace("{0}", "initial"));
                            }
                            else
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[{numValue}]", GetArbitraryTemplate(usage.Value).Replace("{0}", numValue));
                            }
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-number)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-number)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(number:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(number:--my-number)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-number)"));
                        }
                    }

                    if (usage.Value is { InLengthCollection: true, InIntegerCollection: false, InFloatNumberCollection: false, InFlexCollection: false })
                    {
                        var numValue = "4";

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>", usage.Value.Template.Replace("{0}", "<number>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}{numValue}", usage.Value.Template.Replace("{0}", numValue));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", usage.Value.Template.Replace("{0}", "<percentage>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", usage.Value.Template.Replace("{0}", "50%"));

                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.InAbstractValueCollection)
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[initial]", GetArbitraryTemplate(usage.Value).Replace("{0}", "initial"));
                            }
                            else
                            {
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[{numValue}]", GetArbitraryTemplate(usage.Value).Replace("{0}", numValue));
                            }
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(length:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(length:--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));
                        }
                    }
                    
                    if (usage.Value.InPercentageCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<percentage>", usage.Value.Template.Replace("{0}", "<percentage>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}42%", usage.Value.Template.Replace("{0}", "42%"));

                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[42%]", GetArbitraryTemplate(usage.Value).Replace("{0}", "42%"));
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-percentage)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-percentage)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(percentage:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(percentage:--my-percentage)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-percentage)"));
                        }
                    }
                    
                    if (usage.Value.InRatioCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<ratio>", usage.Value.Template.Replace("{0}", "<ratio>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}16/9", usage.Value.Template.Replace("{0}", "16/9"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<ratio>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<ratio>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[16_/_9]", GetArbitraryTemplate(usage.Value).Replace("{0}", "16 / 9"));
                        
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-ratio)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-ratio)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(ratio:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(ratio:--my-ratio)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-ratio)"));
                    }
                    
                    if (usage.Value.InResolutionCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<resolution>", usage.Value.Template.Replace("{0}", "<resolution>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}1920", usage.Value.Template.Replace("{0}", "1920"));

                        if (usage.Key.StartsWith('-') == false)
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<resolution>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<resolution>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}[1920px]", GetArbitraryTemplate(usage.Value).Replace("{0}", "1920px"));
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-resolution)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-resolution)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(resolution:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(resolution:--my-resolution)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-resolution)"));
                        }
                    }
                    
                    if (usage.Value.InStringCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<text>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<text>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[Hello_World!]", GetArbitraryTemplate(usage.Value).Replace("{0}", "\"Hello World!\""));
                        
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-text)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-text)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(string:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(string:--my-text)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-text)"));
                    }
                    
                    if (usage.Value.InUrlCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<url>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<url>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[/images/logo.jpg]", GetArbitraryTemplate(usage.Value).Replace("{0}", "url(\"/images/logo.jpg\")"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-image)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-image)"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(url:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(url:--my-image)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-image)"));
                    }
                    
                    if (usage.Value is { InAbstractValueCollection: true, InAngleHueCollection: false, InColorCollection: false, InDurationCollection: false, InFlexCollection: false, InFloatNumberCollection: false, InFrequencyCollection: false, InIntegerCollection: false, InLengthCollection: false, InPercentageCollection: false, InRatioCollection: false, InResolutionCollection: false, InSimpleUtilityCollection: false, InStringCollection: false, InUrlCollection: false })
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]", GetArbitraryTemplate(usage.Value).Replace("{0}", "<value>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}[initial]", GetArbitraryTemplate(usage.Value).Replace("{0}", "initial"));

                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-value)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-value)"));
                    }
                }
                else
                {
                    usage.Value.DocDefinitions.Add(usage.Key, usage.Value.Template);
                }
            }
            
            #endregion
            
            exportItems.Add(exportItem);
        }

        var json = JsonSerializer.Serialize(exportItems, Jso);

        return json;
    }

    public string ExportColorDefinitions(AppRunner appRunner)
    {
        var json = JsonSerializer.Serialize(appRunner.Library.ColorsByName, Jso);

        return json;
    }

    public string ExportCssCustomProperties(AppRunner appRunner)
    {
        var json = JsonSerializer.Serialize(appRunner.AppRunnerSettings.SfumatoBlockItems, Jso);

        return json;
    }

    private string GetArbitraryTemplate(ClassDefinition definition)
    {
        return string.IsNullOrEmpty(definition.ArbitraryCssValueTemplate) ? definition.Template : definition.ArbitraryCssValueTemplate;
    }
    
    private JsonSerializerOptions Jso { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}