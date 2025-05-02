// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BoxShadow : ClassDictionaryBase
{
    public BoxShadow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shadow-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        box-shadow: {0};
                        """,
                    ArbitraryModifierTemplate =
                        """
                        box-shadow: {0};
                        """,
                }
            },
            {
                "shadow-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = 
                        """
                        box-shadow: 0 0 #0000;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--shadow-")))
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
                    box-shadow: {0};
                    """,
                UsesCssCustomProperties = [ text.Key, "--sf-shadow-color" ]
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
