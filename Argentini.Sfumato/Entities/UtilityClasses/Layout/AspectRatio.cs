// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class AspectRatio : ClassDictionaryBase
{
    public AspectRatio()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "aspect-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InRatioCollection = true,
                    Template =
                        """
                        aspect-ratio: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        aspect-ratio: {0};
                        """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--aspect-")))
        {
            var key = item.Key.Trim('-');
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     aspect-ratio: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}