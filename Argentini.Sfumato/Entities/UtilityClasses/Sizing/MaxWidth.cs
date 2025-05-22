// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Sizing;

public sealed class MaxWidth : ClassDictionaryBase
{
    public MaxWidth()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "max-w-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               max-width: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        max-width: {0};
                        """,
                }
            },
            {
                "container", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               width: 100%;
                               @variant sm { max-width: var(--breakpoint-sm); }
                               @variant md { max-width: var(--breakpoint-md); }
                               @variant lg { max-width: var(--breakpoint-lg); }
                               @variant xl { max-width: var(--breakpoint-xl); }
                               @variant 2xl { max-width: var(--breakpoint-2xl); }
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
        {
            var key = item.Key.Trim('-').Replace("container", "max-w");
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     max-width: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }

        foreach (var item in WidthSizes)
        {
            var key = $"max-w-{item.Key}";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     max-width: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
