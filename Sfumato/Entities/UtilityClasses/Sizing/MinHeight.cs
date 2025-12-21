// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class MinHeight : ClassDictionaryBase
{
    public MinHeight()
    {
        Group = "min-height";
        Description = "Utilities for setting minimum height of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "min-h-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-height: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-height: {0};
                        """,
                }
            },
            {
                "min-h-safe-dvh", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-height: calc(100dvh - env(safe-area-inset-top) - env(safe-area-inset-bottom));
                               """
                }
            },
            {
                "min-h-safe-dvh-t", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-height: calc(100dvh - env(safe-area-inset-top));
                               """
                }
            },
            {
                "min-h-safe-dvh-t-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-height: calc(100dvh - env(safe-area-inset-top, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-height: calc(100dvh - env(safe-area-inset-top, {0}));
                        """,
                }
            },
            {
                "min-h-safe-dvh-b", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               min-height: calc(100dvh - env(safe-area-inset-bottom));
                               """
                }
            },
            {
                "min-h-safe-dvh-b-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               min-height: calc(100dvh - env(safe-area-inset-bottom, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        min-height: calc(100dvh - env(safe-area-inset-bottom, {0}));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in HeightSizes)
        {
            var key = $"min-h-{item.Key}";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     min-height: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
