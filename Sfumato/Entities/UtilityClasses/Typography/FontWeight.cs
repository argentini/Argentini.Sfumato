// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontWeight : ClassDictionaryBase
{
    public FontWeight()
    {
        Group = "font-weight";
        Description = "Utilities for setting font weight.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "font-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InIntegerCollection = true,
                    Template = """
                               font-weight: {0};
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var font in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--font-weight-")))
        {
            var key = font.Key.Replace("--font-weight", "font");
            var value = new ClassDefinition
            {
                SelectorSort = 1,
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                    --sf-font-weight: var({font.Key});
                    font-weight: var({font.Key});
                    """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}