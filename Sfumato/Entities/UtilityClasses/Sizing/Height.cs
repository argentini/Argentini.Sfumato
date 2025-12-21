// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Sizing;

public sealed class Height : ClassDictionaryBase
{
    public Height()
    {
        Group = "height";
        Description = "Utilities for specifying element height.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "h-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               height: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        height: {0};
                        """,
                }
            },
            {
                "h-safe-dvh", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               height: calc(100dvh - env(safe-area-inset-top) - env(safe-area-inset-bottom));
                               """
                }
            },
            {
                "h-safe-dvh-t", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               height: calc(100dvh - env(safe-area-inset-top));
                               """
                }
            },
            {
                "h-safe-dvh-t-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               height: calc(100dvh - env(safe-area-inset-top, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        height: calc(100dvh - env(safe-area-inset-top, {0}));
                        """,
                }
            },
            {
                "h-safe-dvh-b", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               height: calc(100dvh - env(safe-area-inset-bottom));
                               """
                }
            },
            {
                "h-safe-dvh-b-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               height: calc(100dvh - env(safe-area-inset-bottom, calc(var(--spacing) * {0})));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        height: calc(100dvh - env(safe-area-inset-bottom, {0}));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in HeightSizes)
        {
            var key = $"h-{item.Key}";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = 
                    $"""
                     height: {item.Value};
                     """,
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key, null);
            else
                appRunner.Library.SimpleClasses[key] = value;
        }
    }
}
