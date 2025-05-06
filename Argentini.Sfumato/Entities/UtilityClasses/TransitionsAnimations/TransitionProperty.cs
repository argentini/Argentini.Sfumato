// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAnimations;

public sealed class TransitionProperty : ClassDictionaryBase
{
    public TransitionProperty()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transition-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        transition-property: {0};
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: color, background-color, border-color, outline-color, text-decoration-color, fill, stroke, --sf-gradient-from, --sf-gradient-via, --sf-gradient-to, opacity, box-shadow, transform, translate, scale, rotate, filter, -webkit-backdrop-filter, backdrop-filter;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-all", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: all;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-colors", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: color, background-color, border-color, outline-color, text-decoration-color, fill, stroke, --sf-gradient-from, --sf-gradient-via, --sf-gradient-to;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-opacity", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: opacity;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-shadow", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: box-shadow;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-transform", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: transform, translate, scale, rotate;
                        transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                        transition-duration: var(--sf-duration, var(--default-transition-duration));
                        """,
                }
            },
            {
                "transition-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-property: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}