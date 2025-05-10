// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollPadding : ClassDictionaryBase
{
    public ScrollPadding()
    {
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
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}