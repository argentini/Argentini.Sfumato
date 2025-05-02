// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TextShadow : ClassDictionaryBase
{
    public TextShadow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-shadow-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        text-shadow: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        text-shadow: {0};
                        """,
                }
            },
            {
                "text-shadow-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = 
                        """
                        text-shadow: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--text-shadow-")))
        {
            var key = text.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesAbstractValue = true,
                Template =
                    $"""
                    text-shadow: var({text.Key});
                    """,
                ArbitraryCssValueTemplate = 
                    """
                    text-shadow: {0};
                    """,
                UsesCssCustomProperties = [ text.Key, "--sf-text-shadow-color" ]
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
