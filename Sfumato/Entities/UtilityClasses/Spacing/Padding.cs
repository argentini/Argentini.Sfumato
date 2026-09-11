// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Spacing;

public sealed class Padding : ClassDictionaryBase
{
    public Padding()
    {
        Group = "padding";
        Description = "Utilities for setting the padding inside elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region p
            
            {
                "p-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding: 1px;
                               """
                }
            },
            {
                "-p-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding: -1px;
                               """
                }
            },
            {
                "p-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding: {0};
                        """,
                }
            },
            {
                "-p-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region px
            
            {
                "px-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline: 1px;
                               """
                }
            },
            {
                "-px-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline: -1px;
                               """
                }
            },
            {
                "px-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline: {0};
                        """,
                }
            },
            {
                "-px-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region py
            
            {
                "py-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-block: 1px;
                               """
                }
            },
            {
                "-py-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-block: -1px;
                               """
                }
            },
            {
                "py-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-block: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-block: {0};
                        """,
                }
            },
            {
                "-py-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-block: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-block: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region ps
            
            {
                "ps-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline-start: 1px;
                               """
                }
            },
            {
                "-ps-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline-start: -1px;
                               """
                }
            },
            {
                "ps-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline-start: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-start: {0};
                        """,
                }
            },
            {
                "-ps-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline-start: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-start: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region pe
            
            {
                "pe-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline-end: 1px;
                               """
                }
            },
            {
                "-pe-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-inline-end: -1px;
                               """
                }
            },
            {
                "pe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline-end: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-end: {0};
                        """,
                }
            },
            {
                "-pe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-inline-end: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-end: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region pt
            
            {
                "pt-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-top: 1px;
                               """
                }
            },
            {
                "-pt-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-top: -1px;
                               """
                }
            },
            {
                "pt-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-top: env(safe-area-inset-top);
                               """
                }
            },
            {
                "pt-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-top: env(safe-area-inset-top, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-top: env(safe-area-inset-top, {0});
                        """,
                }
            },
            {
                "pt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-top: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-top: {0};
                        """,
                }
            },
            {
                "-pt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-top: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-top: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region pr
            
            {
                "pr-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-right: 1px;
                               """
                }
            },
            {
                "-pr-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-right: -1px;
                               """
                }
            },
            {
                "pr-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-right: env(safe-area-inset-right);
                               """
                }
            },
            {
                "pr-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-right: env(safe-area-inset-right, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-right: env(safe-area-inset-right, {0});
                        """,
                }
            },
            {
                "pr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-right: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-right: {0};
                        """,
                }
            },
            {
                "-pr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-right: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-right: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region pb
            
            {
                "pb-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-bottom: 1px;
                               """
                }
            },
            {
                "-pb-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-bottom: -1px;
                               """
                }
            },
            {
                "pb-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-bottom: env(safe-area-inset-bottom);
                               """
                }
            },
            {
                "pb-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-bottom: env(safe-area-inset-bottom, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-bottom: env(safe-area-inset-bottom, {0});
                        """,
                }
            },
            {
                "pb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-bottom: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-bottom: {0};
                        """,
                }
            },
            {
                "-pb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-bottom: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-bottom: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region pl
            
            {
                "pl-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-left: 1px;
                               """
                }
            },
            {
                "-pl-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-left: -1px;
                               """
                }
            },
            {
                "pl-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               padding-left: env(safe-area-inset-left);
                               """
                }
            },
            {
                "pl-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-left: env(safe-area-inset-left, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-left: env(safe-area-inset-left, {0});
                        """,
                }
            },
            {
                "pl-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-left: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-left: {0};
                        """,
                }
            },
            {
                "-pl-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               padding-left: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-left: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });

        foreach (var key in Data.Keys.Where(key => key.StartsWith("-p", StringComparison.Ordinal)).ToArray())
            Data.Remove(key);

        AddLogicalPadding("pbs", "padding-block-start");
        AddLogicalPadding("pbe", "padding-block-end");
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--spacing-", StringComparison.Ordinal)))
        {
            var suffix = item.Key[10..];

            AddThemeValue(appRunner, $"pbs-{suffix}", $"padding-block-start: var({item.Key});");
            AddThemeValue(appRunner, $"pbe-{suffix}", $"padding-block-end: var({item.Key});");
        }
    }

    private void AddLogicalPadding(string name, string property)
    {
        Data.Add($"{name}-px", new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: 1px;",
        });
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
