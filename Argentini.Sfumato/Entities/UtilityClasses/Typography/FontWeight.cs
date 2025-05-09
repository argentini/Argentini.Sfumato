// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontWeight : ClassDictionaryBase
{
    public FontWeight()
    {
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
                IsSimpleUtility = true,
                Template = 
                    $"font-weight: var({font.Key});",
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}