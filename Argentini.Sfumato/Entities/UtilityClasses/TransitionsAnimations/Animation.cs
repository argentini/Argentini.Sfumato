// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAnimations;

public sealed class Animation : ClassDictionaryBase
{
    public Animation()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "animate-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        animation: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        animation: {0};
                        """,
                }
            },
            {
                "animate-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        animation: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--animate-")))
        {
            var key = item.Key.Trim('-');
            var value = new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = 
                    $"""
                     animation: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;

            var keyframesKey = $"@keyframes {key.Split('-')[^1]}";

            appRunner.UsedCss.TryAdd(keyframesKey, $"{{ {item.Value.TrimEnd(';')}; }}");
        }
    }
}