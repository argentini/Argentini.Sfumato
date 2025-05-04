// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetShadow : ClassDictionaryBase
{
    public InsetShadow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-shadow-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        box-shadow: inset {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        box-shadow: inset {0};
                        """,
                }
            },
            {
                "inset-shadow-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = 
                        """
                        box-shadow: inset 0 0 #0000;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--inset-shadow-")))
        {
            var key = text.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesAbstractValue = true,
                Template =
                    $"""
                    box-shadow: var({text.Key});
                    """,
                ArbitraryModifierTemplate =
                    """
                    box-shadow: inset {0};
                    """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
