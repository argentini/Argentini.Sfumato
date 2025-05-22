// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Perspective : ClassDictionaryBase
{
    public Perspective()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "perspective-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        perspective: {0};
                        """,
                }
            },
            {
                "perspective-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--perspective-")))
        {
            var key = item.Key.Trim('-');
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     perspective: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}