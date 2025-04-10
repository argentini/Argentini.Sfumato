// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontFamily : ClassDictionaryBase
{
    public FontFamily()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "font-", new ClassDefinition
                {
                    Template = """
                               font-family: {0};
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var font in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--font-") && i.Key.LastIndexOf("--", StringComparison.Ordinal) == 0))
        {
            var key = font.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"font-family: {font.Value};"
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}