// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class MaxWidth : ClassDictionaryBase
{
    public MaxWidth()
    {
        Group = "max-width";
        Description = "Utilities for setting maximum width of elements.";
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
                "max-w-safe-dvw", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               max-width: calc(100dvw - env(safe-area-inset-left) - env(safe-area-inset-right));
                               """
                }
            },
            {
                "max-w-safe-dvw-l", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               max-width: calc(100dvw - env(safe-area-inset-left));
                               """
                }
            },
            {
                "max-w-safe-dvw-l-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               max-width: calc(100dvw - env(safe-area-inset-left, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        max-width: calc(100dvw - env(safe-area-inset-left, {0}));
                        """,
                }
            },
            {
                "max-w-safe-dvw-r", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               max-width: calc(100dvw - env(safe-area-inset-right));
                               """
                }
            },
            {
                "max-w-safe-dvw-r-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               max-width: calc(100dvw - env(safe-area-inset-right, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        max-width: calc(100dvw - env(safe-area-inset-right, {0}));
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
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
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
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
