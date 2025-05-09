// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class DropShadow : ClassDictionaryBase
{
    public DropShadow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "drop-shadow-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        --sf-drop-shadow-size: drop-shadow({0});
                        --sf-drop-shadow: drop-shadow(var(--sf-drop-shadow-size));
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
            {
                "drop-shadow-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-drop-shadow: ;
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var text in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--drop-shadow-")))
        {
            var key = text.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template =
                    $"""
                    --sf-drop-shadow-size: drop-shadow({text.Value});
                    --sf-drop-shadow: drop-shadow(var({text.Key}));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}