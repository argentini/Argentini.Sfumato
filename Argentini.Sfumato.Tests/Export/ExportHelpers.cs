using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato.Tests.Export;

public static class ExportHelpers
{
    public static string ExportUtilityClassDefinitions(this AppRunner appRunner)
    {
        var exportItems = new List<ExportItem>();
        var derivedTypes = Assembly.GetAssembly(typeof(ClassDictionaryBase))
            ?.GetTypes()
            .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
            .OrderBy(t => t.AssemblyQualifiedName)
            .ToList() ?? [];
        var groups = new Dictionary<string,string>();

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

            appRunner.Library.SimpleClasses.Clear();
            appRunner.Library.AbstractClasses.Clear();
            appRunner.Library.AngleHueClasses.Clear();
            appRunner.Library.ColorClasses.Clear();
            appRunner.Library.DurationClasses.Clear();
            appRunner.Library.FlexClasses.Clear();
            appRunner.Library.FloatNumberClasses.Clear();
            appRunner.Library.FrequencyClasses.Clear();
            appRunner.Library.IntegerClasses.Clear();
            appRunner.Library.LengthClasses.Clear();
            appRunner.Library.PercentageClasses.Clear();
            appRunner.Library.RatioClasses.Clear();
            appRunner.Library.ResolutionClasses.Clear();
            appRunner.Library.StringClasses.Clear();
            appRunner.Library.UrlClasses.Clear();
            
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
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.SimpleClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);
            
            foreach (var item in appRunner.Library.AbstractClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.AngleHueClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.ColorClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.DurationClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.FlexClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.FloatNumberClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.FrequencyClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.IntegerClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.LengthClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.PercentageClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.RatioClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.ResolutionClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.StringClasses)
                if (item.Value.IsRazorSyntax == false)
                    exportItem.Usages.Add(item.Key, item.Value);

            foreach (var item in appRunner.Library.UrlClasses)
                if (item.Value.IsRazorSyntax == false)
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
                                if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", GetArbitraryTemplate(usage.Value).Replace("{0}", "<percentage>")))
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", GetArbitraryTemplate(usage.Value).Replace("{0}", "50%"));
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
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", GetArbitraryTemplate(usage.Value).Replace("{0}", "<percentage>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", GetArbitraryTemplate(usage.Value).Replace("{0}", "50%"));
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

                        if (usage.Key == "text-")
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<number>/<modifier>", usage.Value.ModifierTemplate.Replace("{0}", "<number>").Replace("{1}", "<modifier>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}{numValue}/5", usage.Value.ModifierTemplate.Replace("{0}", numValue).Replace("{1}", "5"));
                        }
                        else
                        {
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<fraction>", GetArbitraryTemplate(usage.Value).Replace("{0}", "<percentage>")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}1/2", GetArbitraryTemplate(usage.Value).Replace("{0}", "50%"));
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
                                    usage.Value.DocExamples.TryAdd($"{usage.Key}[1rem]", GetArbitraryTemplate(usage.Value).Replace("{0}", "1rem"));

                                if (usage.Key == "text-")
                                {
                                    if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]/<modifier>", usage.Value.ArbitraryCssValueWithModifierTemplate.Replace("{0}", "<value>").Replace("{1}", "<modifier>")))
                                        usage.Value.DocExamples.TryAdd($"{usage.Key}[1rem]/5", usage.Value.ArbitraryCssValueWithModifierTemplate.Replace("{0}", "1rem").Replace("{1}", "5"));
                                    
                                    if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}[<value>]/[<modifier>]", usage.Value.ArbitraryCssValueWithArbitraryModifierTemplate.Replace("{0}", "<value>").Replace("{1}", "[<modifier>]")))
                                        usage.Value.DocExamples.TryAdd($"{usage.Key}[1rem]/[1.3]", usage.Value.ArbitraryCssValueWithArbitraryModifierTemplate.Replace("{0}", "1rem").Replace("{1}", "1.3"));
                                }
                            }
                            
                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));

                            if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}(length:<custom-property>)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(<custom-property>)")))
                                usage.Value.DocExamples.TryAdd($"{usage.Key}(length:--my-length)", GetArbitraryTemplate(usage.Value).Replace("{0}", "var(--my-length)"));
                        }
                    }
                    
                    if (usage.Value.InPercentageCollection)
                    {
                        if (usage.Value.DocDefinitions.TryAdd($"{usage.Key}<percentage>", GetArbitraryTemplate(usage.Value).Replace("{0}", "<percentage>")))
                            usage.Value.DocExamples.TryAdd($"{usage.Key}42%", GetArbitraryTemplate(usage.Value).Replace("{0}", "42%"));

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

    public static string ExportColorDefinitions(this AppRunner appRunner)
    {
        var json = JsonSerializer.Serialize(appRunner.Library.ColorsByName.ToDictionary(), Jso);

        return json;
    }

    public static string ExportCssCustomProperties(this AppRunner appRunner)
    {
        var json = JsonSerializer.Serialize(appRunner.AppRunnerSettings.SfumatoBlockItems, Jso);

        return json;
    }

    public static string ExportVariants(this AppRunner appRunner)
    {
        var variants = new Dictionary<string, VariantMetadata>();

        variants.AddRange(appRunner.Library.PseudoclassPrefixes);
        variants.AddRange(appRunner.Library.MediaQueryPrefixes);
        variants.AddRange(appRunner.Library.SupportsQueryPrefixes);
        variants.AddRange(appRunner.Library.StartingStyleQueryPrefixes);
        variants.AddRange(appRunner.Library.ContainerQueryPrefixes);

        foreach (var variant in variants)
        {
            if (variant.Key.EndsWith('-') == false || variant.Value.IsRazorSyntax)
                continue;
            
            if (variant.Value.PrefixType == "pseudoclass")
            {
                if (variant.Key == "data-")
                    variant.Value.SelectorSuffix = $"[{variant.Key}*] /* data attribute exists */";
                else if (variant.Key == "not-data-")
                    variant.Value.SelectorSuffix = $"[{variant.Key}*] /* data attribute does not exist */";
                else if (variant.Key is "peer-aria-" or "not-peer-aria-" or "group-aria-" or "not-group-aria-")
                    variant.Value.SelectorSuffix = $"[{variant.Key}*] /* (e.g. [sort=ascending] for aria-sort=\"ascending\") */";
                else if (variant.Key is "peer-has-" or "not-peer-has-" or "group-has-" or "not-group-has-")
                    variant.Value.SelectorSuffix = $"[{variant.Key}*] /* (e.g. checked) */";
                else if (variant.Key is "peer-" or "not-peer-" or "group-" or "not-group-")
                    variant.Value.SelectorSuffix = $"[{variant.Key}*] /* (e.g. invalid) */";
            }
            else if (variant.Value.PrefixType == "media")
            {
                variant.Value.Statement = variant.Value.Statement.Replace("{0}", "1024px");
            }
        }
        
        var json = JsonSerializer.Serialize(variants, Jso);

        return json;
    }

    private static string GetArbitraryTemplate(ClassDefinition definition)
    {
        return string.IsNullOrEmpty(definition.ArbitraryCssValueTemplate) ? definition.Template : definition.ArbitraryCssValueTemplate;
    }
    
    private static JsonSerializerOptions Jso { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}