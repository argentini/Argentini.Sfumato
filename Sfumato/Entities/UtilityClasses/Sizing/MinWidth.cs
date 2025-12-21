// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class MinWidth : ClassDictionaryBase
{
    public MinWidth()
    {
        Group = "min-width";
        Description = "Utilities for setting minimum width of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "min-w-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-width: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-width: {0};
                        """,
                }
            },
            {
                "min-w-safe-dvw", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-width: calc(100dvw - env(safe-area-inset-left) - env(safe-area-inset-right));
                               """
                }
            },
            {
                "min-w-safe-dvw-l", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-width: calc(100dvw - env(safe-area-inset-left));
                               """
                }
            },
            {
                "min-w-safe-dvw-l-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-width: calc(100dvw - env(safe-area-inset-left, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-width: calc(100dvw - env(safe-area-inset-left, {0}));
                        """,
                }
            },
            {
                "min-w-safe-dvw-r", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-width: calc(100dvw - env(safe-area-inset-right));
                               """
                }
            },
            {
                "min-w-safe-dvw-r-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-width: calc(100dvw - env(safe-area-inset-right, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-width: calc(100dvw - env(safe-area-inset-right, {0}));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
        {
            var key = item.Key.Trim('-').Replace("container", "min-w");
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     min-width: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }

        foreach (var item in WidthSizes)
        {
            var key = $"min-w-{item.Key}";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     min-width: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
