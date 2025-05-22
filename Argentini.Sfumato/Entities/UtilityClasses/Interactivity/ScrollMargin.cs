// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollMargin : ClassDictionaryBase
{
    public ScrollMargin()
    {
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
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}