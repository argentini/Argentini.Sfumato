// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class Width : ClassDictionaryBase
{
    public Width()
    {
        Group = "width";
        Description = "Utilities for specifying element width.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "w-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               width: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        width: {0};
                        """,
                }
            },
            {
                "w-safe-dvw", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               width: calc(100dvw - env(safe-area-inset-left) - env(safe-area-inset-right));
                               """
                }
            },
            {
                "w-safe-dvw-l", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               width: calc(100dvw - env(safe-area-inset-left));
                               """
                }
            },
            {
                "w-safe-dvw-l-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               width: calc(100dvw - env(safe-area-inset-left, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        width: calc(100dvw - env(safe-area-inset-left, {0}));
                        """,
                }
            },
            {
                "w-safe-dvw-r", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               width: calc(100dvw - env(safe-area-inset-right));
                               """
                }
            },
            {
                "w-safe-dvw-r-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               width: calc(100dvw - env(safe-area-inset-right, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        width: calc(100dvw - env(safe-area-inset-right, {0}));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
        {
            var key = item.Key.Trim('-').Replace("container", "w");
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     width: var({item.Key});
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }

        foreach (var item in WidthSizes)
        {
            var key = $"w-{item.Key}";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     width: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
