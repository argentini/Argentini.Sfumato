// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideWidth : ClassDictionaryBase
{
    public DivideWidth()
    {
        Group = "border-width";
        Description = "Utilities for setting divider width between elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-x-", new ClassDefinition
                {
                    InLengthCollection = true,
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
                }
            },
            {
                "divide-x", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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
                    InLengthCollection = true,
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
                }
            },
            {
                "divide-y", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-divide-x-reverse: 1;
                        """,
                }
            },
            {
                "divide-y-reverse", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-divide-y-reverse: 1;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
