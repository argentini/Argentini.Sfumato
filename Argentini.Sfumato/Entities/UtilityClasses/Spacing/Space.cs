// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Spacing;

public sealed class Space : ClassDictionaryBase
{
    public Space()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            #region space-x
            
            {
                "space-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               & > :not(:last-child) {
                                   margin-inline-start: calc(1px * var(--sf-space-x-reverse));
                                   margin-inline-end: calc(1px * calc(1 - var(--sf-space-x-reverse)));
                               }
                               """,
                }
            },
            {
                "-space-x-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               & > :not(:last-child) {
                                   margin-inline-start: calc(-1px * var(--sf-space-x-reverse));
                                   margin-inline-end: calc(-1px * calc(1 - var(--sf-space-x-reverse)));
                               }
                               """,
                }
            },
            {
                "space-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               & > :not(:last-child) {
                                   margin-inline-start: calc(calc(var(--spacing) * {0}) * var(--sf-space-x-reverse));
                                   margin-inline-end: calc(calc(var(--spacing) * {0}) * calc(1 - var(--sf-space-x-reverse)));
                               }
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        & > :not(:last-child) {
                            margin-inline-start: calc({0} * var(--sf-space-x-reverse));
                            margin-inline-end: calc({0} * calc(1 - var(--sf-space-x-reverse)));
                        }
                        """,
                }
            },
            {
                "-space-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               & > :not(:last-child) {
                                   margin-inline-start: calc(calc(var(--spacing) * -{0}) * var(--sf-space-x-reverse));
                                   margin-inline-end: calc(calc(var(--spacing) * -{0}) * calc(1 - var(--sf-space-x-reverse)));
                               }
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        & > :not(:last-child) {
                            margin-inline-start: calc(-1 * {0} * var(--sf-space-x-reverse));
                            margin-inline-end: calc(-1 * {0} * calc(1 - var(--sf-space-x-reverse)));
                        }
                        """,
                }
            },

            #endregion
            
            #region space-y
            
            {
                "space-y-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               & > :not(:first-child) {
                                   margin-block-start: calc(1px * calc(1 - var(--sf-space-y-reverse)));
                                   margin-block-end: calc(1px * var(--sf-space-y-reverse));
                               }
                               """,
                }
            },
            {
                "-space-y-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               & > :not(:first-child) {
                                   margin-block-start: calc(-1px * calc(1 - var(--sf-space-y-reverse)));
                                   margin-block-end: calc(-1px * var(--sf-space-y-reverse));
                               }
                               """,
                }
            },
            {
                "space-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               & > :not(:first-child) {
                                   margin-block-start: calc(calc(var(--spacing) * {0}) * calc(1 - var(--sf-space-y-reverse)));
                                   margin-block-end: calc(calc(var(--spacing) * {0}) * var(--sf-space-y-reverse));
                               }
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        & > :not(:first-child) {
                            margin-block-start: calc({0} * calc(1 - var(--sf-space-y-reverse)));
                            margin-block-end: calc({0} * var(--sf-space-y-reverse));
                        };
                        """,
                }
            },
            {
                "-space-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               & > :not(:first-child) {
                                   margin-block-start: calc(calc(var(--spacing) * -{0}) * calc(1 - var(--sf-space-y-reverse)));
                                   margin-block-end: calc(calc(var(--spacing) * -{0}) * var(--sf-space-y-reverse));
                               }
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        & > :not(:first-child) {
                            margin-block-start: calc(-1 * {0} * calc(1 - var(--sf-space-y-reverse)));
                            margin-block-end: calc(-1 * {0} * var(--sf-space-y-reverse));
                        }
                        """,
                }
            },

            #endregion
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
