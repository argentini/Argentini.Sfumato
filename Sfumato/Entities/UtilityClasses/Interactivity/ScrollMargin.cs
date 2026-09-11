// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollMargin : ClassDictionaryBase
{
    public ScrollMargin()
    {
        Group = "scroll-margin";
        Description = "Utilities for setting margin at scroll snap points.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scroll-m-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin: {0};
                        """,
                }
            },
            {
                "scroll-mx-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-inline: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-inline: {0};
                        """,
                }
            },
            {
                "scroll-my-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-block: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-block: {0};
                        """,
                }
            },
            {
                "scroll-ms-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-inline-start: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-inline-start: {0};
                        """,
                }
            },
            {
                "scroll-me-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-inline-end: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-inline-end: {0};
                        """,
                }
            },
            {
                "scroll-mt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-top: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-top: {0};
                        """,
                }
            },
            {
                "scroll-mt-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-margin-top: env(safe-area-inset-top);
                               """
                }
            },
            {
                "scroll-mt-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-margin-top: env(safe-area-inset-top, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-top: env(safe-area-inset-top, {0});
                        """,
                }
            },
            {
                "scroll-mr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-right: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-right: {0};
                        """,
                }
            },
            {
                "scroll-mr-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-margin-right: env(safe-area-inset-right);
                               """
                }
            },
            {
                "scroll-mr-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-margin-right: env(safe-area-inset-right, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-right: env(safe-area-inset-right, {0});
                        """,
                }
            },
            {
                "scroll-mb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-bottom: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-bottom: {0};
                        """,
                }
            },
            {
                "scroll-mb-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-margin-bottom: env(safe-area-inset-bottom);
                               """
                }
            },
            {
                "scroll-mb-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-margin-bottom: env(safe-area-inset-bottom, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-bottom: env(safe-area-inset-bottom, {0});
                        """,
                }
            },
            {
                "scroll-ml-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        scroll-margin-left: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-left: {0};
                        """,
                }
            },
            {
                "scroll-ml-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-margin-left: env(safe-area-inset-left);
                               """
                }
            },
            {
                "scroll-ml-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               scroll-margin-left: env(safe-area-inset-left, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        scroll-margin-left: env(safe-area-inset-left, {0});
                        """,
                }
            },
        });

        AddLogicalMargin("scroll-mbs", "scroll-margin-block-start");
        AddLogicalMargin("scroll-mbe", "scroll-margin-block-end");
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--spacing-", StringComparison.Ordinal)))
        {
            var suffix = item.Key[10..];

            AddThemeValue(appRunner, $"scroll-mbs-{suffix}", $"scroll-margin-block-start: var({item.Key});");
            AddThemeValue(appRunner, $"-scroll-mbs-{suffix}", $"scroll-margin-block-start: calc(var({item.Key}) * -1);");
            AddThemeValue(appRunner, $"scroll-mbe-{suffix}", $"scroll-margin-block-end: var({item.Key});");
            AddThemeValue(appRunner, $"-scroll-mbe-{suffix}", $"scroll-margin-block-end: calc(var({item.Key}) * -1);");
        }
    }

    private void AddLogicalMargin(string name, string property)
    {
        Data.Add($"{name}-", new ClassDefinition
        {
            InLengthCollection = true,
            Template = $"{property}: calc(var(--spacing) * {{0}});",
            ArbitraryCssValueTemplate = $"{property}: {{0}};",
        });
        Data.Add($"-{name}-", new ClassDefinition
        {
            InLengthCollection = true,
            Template = $"{property}: calc(var(--spacing) * -{{0}});",
            ArbitraryCssValueTemplate = $"{property}: calc({{0}} * -1);",
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
