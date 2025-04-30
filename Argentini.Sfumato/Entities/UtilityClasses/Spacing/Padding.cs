// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Spacing;

public sealed class Padding : ClassDictionaryBase
{
    public Padding()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region p
            
            {
                "p-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding: 1px;
                               """
                }
            },
            {
                "-p-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding: -1px;
                               """
                }
            },
            {
                "p-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-p-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region px
            
            {
                "px-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline: 1px;
                               """
                }
            },
            {
                "-px-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline: -1px;
                               """
                }
            },
            {
                "px-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-px-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region py
            
            {
                "py-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-block: 1px;
                               """
                }
            },
            {
                "-py-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-block: -1px;
                               """
                }
            },
            {
                "py-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-block: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-block: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-py-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-block: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-block: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region ps
            
            {
                "ps-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline-start: 1px;
                               """
                }
            },
            {
                "-ps-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline-start: -1px;
                               """
                }
            },
            {
                "ps-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline-start: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-start: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-ps-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline-start: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-start: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region pe
            
            {
                "pe-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline-end: 1px;
                               """
                }
            },
            {
                "-pe-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-inline-end: -1px;
                               """
                }
            },
            {
                "pe-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline-end: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-end: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-pe-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-inline-end: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-inline-end: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region pt
            
            {
                "pt-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-top: 1px;
                               """
                }
            },
            {
                "-pt-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-top: -1px;
                               """
                }
            },
            {
                "pt-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-top: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-top: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-pt-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-top: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-top: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region pr
            
            {
                "pr-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-right: 1px;
                               """
                }
            },
            {
                "-pr-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-right: -1px;
                               """
                }
            },
            {
                "pr-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-right: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-right: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-pr-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-right: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-right: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region pb
            
            {
                "pb-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-bottom: 1px;
                               """
                }
            },
            {
                "-pb-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-bottom: -1px;
                               """
                }
            },
            {
                "pb-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-bottom: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-bottom: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-pb-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-bottom: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-bottom: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
            
            #region pl
            
            {
                "pl-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-left: 1px;
                               """
                }
            },
            {
                "-pl-px", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               padding-left: -1px;
                               """
                }
            },
            {
                "pl-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-left: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-left: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "-pl-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               padding-left: calc(var(--spacing) * -{0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        padding-left: calc({0} * -1);
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
