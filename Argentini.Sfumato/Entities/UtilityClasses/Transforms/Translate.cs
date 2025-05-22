// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Translate : ClassDictionaryBase
{
    public Translate()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "translate-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-x: calc(var(--spacing) * {0});
                        --sf-translate-y: calc(var(--spacing) * {0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-x: {0};
                        --sf-translate-y: {0};
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "-translate-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-x: calc(var(--spacing) * -{0});
                        --sf-translate-y: calc(var(--spacing) * -{0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-x: calc({0} * -1);
                        --sf-translate-y: calc({0} * -1);
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "translate-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: none;
                        """,
                }
            },
            {
                "translate-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: 100% 100%;
                        """,
                }
            },
            {
                "-translate-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: -100% -100%;
                        """,
                }
            },
            {
                "translate-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: 1px 1px;
                        """,
                }
            },
            {
                "-translate-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: -1px -1px;
                        """,
                }
            },

            
            
            
            {
                "translate-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-x: calc(var(--spacing) * {0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-x: {0};
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "-translate-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-x: calc(var(--spacing) * -{0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-x: calc({0} * -1);
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "translate-x-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: 100% var(--sf-translate-y);
                        """,
                }
            },
            {
                "-translate-x-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: -100% var(--sf-translate-y);
                        """,
                }
            },
            {
                "translate-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: 1px var(--sf-translate-y);
                        """,
                }
            },
            {
                "-translate-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: -1px var(--sf-translate-y);
                        """,
                }
            },
            
            
            
            {
                "translate-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-y: calc(var(--spacing) * {0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-y: {0};
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "-translate-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-y: calc(var(--spacing) * -{0});
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-y: calc({0} * -1);
                        translate: var(--sf-translate-x) var(--sf-translate-y);
                        """,
                }
            },
            {
                "translate-y-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) 100%;
                        """,
                }
            },
            {
                "-translate-y-full", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) -100%;
                        """,
                }
            },
            {
                "translate-y-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) 1px;
                        """,
                }
            },
            {
                "-translate-y-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) -1px;
                        """,
                }
            },
            
            
            
            {
                "translate-z-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-z: calc(var(--spacing) * {0});
                        translate: var(--sf-translate-x) var(--sf-translate-y) var(--sf-translate-z);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-z: {0};
                        translate: var(--sf-translate-x) var(--sf-translate-y) var(--sf-translate-z);
                        """,
                }
            },
            {
                "-translate-z-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-translate-z: calc(var(--spacing) * -{0});
                        translate: var(--sf-translate-x) var(--sf-translate-y) var(--sf-translate-z);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-translate-z: calc({0} * -1);
                        translate: var(--sf-translate-x) var(--sf-translate-y) var(--sf-translate-z);
                        """,
                }
            },
            {
                "translate-z-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) var(--sf-translate-y) 1px;
                        """,
                }
            },
            {
                "-translate-z-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        translate: var(--sf-translate-x) var(--sf-translate-y) -1px;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}