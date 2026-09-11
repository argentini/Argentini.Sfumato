// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollPadding : ClassDictionaryBase
{
    public ScrollPadding()
    {
        Group = "scroll-padding";
        Description = "Utilities for configuring scroll padding.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scroll-p-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding: {0};
                        """,
                }
            },
            {
                "scroll-px-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-inline: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-inline: {0};
                        """,
                }
            },
            {
                "scroll-py-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-block: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-block: {0};
                        """,
                }
            },
            {
                "scroll-ps-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-inline-start: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-inline-start: {0};
                        """,
                }
            },
            {
                "scroll-pe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-inline-end: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-inline-end: {0};
                        """,
                }
            },
            {
                "scroll-pt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-top: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-top: {0};
                        """,
                }
            },
            {
                "scroll-pt-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-padding-top: env(safe-area-inset-top);
                               """
                }
            },
            {
                "scroll-pt-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-padding-top: env(safe-area-inset-top, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-top: env(safe-area-inset-top, {0});
                        """,
                }
            },
            {
                "scroll-pr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-right: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-right: {0};
                        """,
                }
            },
            {
                "scroll-pr-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-padding-right: env(safe-area-inset-right);
                               """
                }
            },
            {
                "scroll-pr-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-padding-right: env(safe-area-inset-right, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-right: env(safe-area-inset-right, {0});
                        """,
                }
            },
            {
                "scroll-pb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-bottom: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-bottom: {0};
                        """,
                }
            },
            {
                "scroll-pb-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-padding-bottom: env(safe-area-inset-bottom);
                               """
                }
            },
            {
                "scroll-pb-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-padding-bottom: env(safe-area-inset-bottom, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-bottom: env(safe-area-inset-bottom, {0});
                        """,
                }
            },
            {
                "scroll-pl-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-padding-left: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-left: {0};
                        """,
                }
            },
            {
                "scroll-pl-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-padding-left: env(safe-area-inset-left);
                               """
                }
            },
            {
                "scroll-pl-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-padding-left: env(safe-area-inset-left, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-padding-left: env(safe-area-inset-left, {0});
                        """,
                }
            },
        });

        AddLogicalPadding("scroll-pbs", "scroll-padding-block-start");
        AddLogicalPadding("scroll-pbe", "scroll-padding-block-end");
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--spacing-", StringComparison.Ordinal)))
        {
            var suffix = item.Key[10..];

            AddThemeValue(appRunner, $"scroll-pbs-{suffix}", $"scroll-padding-block-start: var({item.Key});");
            AddThemeValue(appRunner, $"scroll-pbe-{suffix}", $"scroll-padding-block-end: var({item.Key});");
        }
    }

    private void AddLogicalPadding(string name, string property)
    {
        Data.Add($"{name}-", new ClassDefinition
        {
            InLengthCollection = true,
            Template = $"{property}: calc(var(--spacing) * {{0}});",
            ArbitraryCssValueTemplate = $"{property}: {{0}};",
        });
    }

    private static void AddThemeValue(AppRunner appRunner, string name, string template)
    {
        var definition = new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = template,
        };

        if (appRunner.Library.SimpleClasses.TryAdd(name, definition))
            appRunner.Library.ScannerClassNamePrefixes.Insert(name, null);
        else
            appRunner.Library.SimpleClasses[name] = definition;
    }
}
