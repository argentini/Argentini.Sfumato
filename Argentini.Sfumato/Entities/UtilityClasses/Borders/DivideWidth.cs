// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideWidth : ClassDictionaryBase
{
    public DivideWidth()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-x-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesDimensionLength = true,
                    Template = 
                        """
                        & > :not(:last-child) {
                            border-inline-start-width: calc({0}px * var(--sf-divide-x-reverse));
                            border-inline-end-width: calc({0}px * calc(1 - var(--sf-divide-x-reverse)))
                        }
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        & > :not(:last-child) {
                            border-inline-start-width: 0px;
                            border-inline-end-width: {0};
                        }
                        """,
                    UsesCssCustomProperties = [ "--sf-divide-x-reverse" ]
                }
            },
            {
                "divide-x", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-inline-start-width: 0px;
                            border-inline-end-width: 1px;
                        }
                        """,
                }
            },
            {
                "divide-y-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesDimensionLength = true,
                    Template = 
                        """
                        & > :not(:last-child) {
                            border-top-width: calc({0}px * var(--sf-divide-y-reverse));
                            border-bottom-width: calc({0}px * calc(1 - var(--sf-divide-y-reverse)))
                        }
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        & > :not(:last-child) {
                            border-top-width: 0px;
                            border-bottom-width: {0};
                        }
                        """,
                    UsesCssCustomProperties = [ "--sf-divide-y-reverse" ]
                }
            },
            {
                "divide-y", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-top-width: 0px;
                            border-bottom-width: 1px;
                        }
                        """,
                }
            },
            {
                "divide-x-reverse", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-divide-x-reverse: 1;
                        """,
                    UsesCssCustomProperties = [ "--sf-divide-x-reverse" ]
                }
            },
            {
                "divide-y-reverse", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-divide-y-reverse: 1;
                        """,
                    UsesCssCustomProperties = [ "--sf-divide-y-reverse" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
