// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Tables;

public sealed class BorderSpacing : ClassDictionaryBase
{
    public BorderSpacing()
    {
        Group = "border-spacing";
        Description = "Utilities for setting spacing between table cell borders.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "border-spacing-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-border-spacing-x: calc(var(--spacing) * {0});
                        --sf-border-spacing-y: calc(var(--spacing) * {0});
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-border-spacing-x: {0};
                        --sf-border-spacing-y: {0};
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                }
            },
            {
                "border-spacing-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-border-spacing-x: calc(var(--spacing) * {0});
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-border-spacing-x: {0};
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                }
            },
            {
                "border-spacing-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-border-spacing-y: calc(var(--spacing) * {0});
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-border-spacing-y: {0};
                        border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}