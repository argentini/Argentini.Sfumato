// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Spacing;

public sealed class Margin : ClassDictionaryBase
{
    public Margin()
    {
        Group = "margin";
        GroupDescription = "Utilities for setting the margin around elements.";
        Description = "Utilities for setting the margin around elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region m
            
            {
                "m-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin: 1px;
                               """
                }
            },
            {
                "-m-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin: -1px;
                               """
                }
            },
            {
                "m-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin: auto;
                               """
                }
            },
            {
                "m-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin: {0};
                        """,
                }
            },
            {
                "-m-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region mx
            
            {
                "mx-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline: 1px;
                               """
                }
            },
            {
                "-mx-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline: -1px;
                               """
                }
            },
            {
                "mx-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline: auto;
                               """
                }
            },
            {
                "mx-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline: {0};
                        """,
                }
            },
            {
                "-mx-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region my
            
            {
                "my-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-block: 1px;
                               """
                }
            },
            {
                "-my-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-block: -1px;
                               """
                }
            },
            {
                "my-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-block: auto;
                               """
                }
            },
            {
                "my-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-block: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-block: {0};
                        """,
                }
            },
            {
                "-my-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-block: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-block: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region ms
            
            {
                "ms-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-start: 1px;
                               """
                }
            },
            {
                "-ms-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-start: -1px;
                               """
                }
            },
            {
                "ms-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-start: auto;
                               """
                }
            },
            {
                "ms-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline-start: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline-start: {0};
                        """,
                }
            },
            {
                "-ms-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline-start: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline-start: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region me
            
            {
                "me-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-end: 1px;
                               """
                }
            },
            {
                "-me-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-end: -1px;
                               """
                }
            },
            {
                "me-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-inline-end: auto;
                               """
                }
            },
            {
                "me-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline-end: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline-end: {0};
                        """,
                }
            },
            {
                "-me-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-inline-end: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-inline-end: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region mt
            
            {
                "mt-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-top: 1px;
                               """
                }
            },
            {
                "-mt-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-top: -1px;
                               """
                }
            },
            {
                "mt-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-top: auto;
                               """
                }
            },
            {
                "mt-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-top: env(safe-area-inset-top);
                               """
                }
            },
            {
                "mt-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-top: env(safe-area-inset-top, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-top: env(safe-area-inset-top, {0});
                        """,
                }
            },
            {
                "mt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-top: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-top: {0};
                        """,
                }
            },
            {
                "-mt-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-top: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-top: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region mr
            
            {
                "mr-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-right: 1px;
                               """
                }
            },
            {
                "-mr-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-right: -1px;
                               """
                }
            },
            {
                "mr-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-right: auto;
                               """
                }
            },
            {
                "mr-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-right: env(safe-area-inset-right);
                               """
                }
            },
            {
                "mr-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-right: env(safe-area-inset-right, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-right: env(safe-area-inset-right, {0});
                        """,
                }
            },
            {
                "mr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-right: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-right: {0};
                        """,
                }
            },
            {
                "-mr-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-right: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-right: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region mb
            
            {
                "mb-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-bottom: 1px;
                               """
                }
            },
            {
                "-mb-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-bottom: -1px;
                               """
                }
            },
            {
                "mb-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-bottom: auto;
                               """
                }
            },
            {
                "mb-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-bottom: env(safe-area-inset-bottom);
                               """
                }
            },
            {
                "mb-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-bottom: env(safe-area-inset-bottom, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-bottom: env(safe-area-inset-bottom, {0});
                        """,
                }
            },
            {
                "mb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-bottom: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-bottom: {0};
                        """,
                }
            },
            {
                "-mb-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-bottom: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-bottom: calc({0} * -1);
                        """,
                }
            },

            #endregion
            
            #region ml
            
            {
                "ml-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-left: 1px;
                               """
                }
            },
            {
                "-ml-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-left: -1px;
                               """
                }
            },
            {
                "ml-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-left: auto;
                               """
                }
            },
            {
                "ml-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               margin-left: env(safe-area-inset-left);
                               """
                }
            },
            {
                "ml-safe-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-left: env(safe-area-inset-left, calc(var(--spacing) * {0}));
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-left: env(safe-area-inset-left, {0});
                        """,
                }
            },
            {
                "ml-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-left: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-left: {0};
                        """,
                }
            },
            {
                "-ml-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               margin-left: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        margin-left: calc({0} * -1);
                        """,
                }
            },

            #endregion
        });

        AddLogicalMargin("mbs", "margin-block-start");
        AddLogicalMargin("mbe", "margin-block-end");
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var item in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(item => item.Key.StartsWith("--spacing-", StringComparison.Ordinal)))
        {
            var suffix = item.Key[10..];

            AddThemeValue(appRunner, $"mbs-{suffix}", $"margin-block-start: var({item.Key});");
            AddThemeValue(appRunner, $"-mbs-{suffix}", $"margin-block-start: calc(var({item.Key}) * -1);");
            AddThemeValue(appRunner, $"mbe-{suffix}", $"margin-block-end: var({item.Key});");
            AddThemeValue(appRunner, $"-mbe-{suffix}", $"margin-block-end: calc(var({item.Key}) * -1);");
        }
    }

    private void AddLogicalMargin(string name, string property)
    {
        Data.Add($"{name}-px", new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: 1px;",
        });
        Data.Add($"-{name}-px", new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: -1px;",
        });
        Data.Add($"{name}-auto", new ClassDefinition
        {
            InSimpleUtilityCollection = true,
            Template = $"{property}: auto;",
        });
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
