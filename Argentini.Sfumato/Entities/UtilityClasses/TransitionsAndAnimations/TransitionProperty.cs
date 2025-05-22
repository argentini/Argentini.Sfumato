// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionProperty : ClassDictionaryBase
{
    public TransitionProperty()
    {
        Group = "transition-property";
        Description = "Utilities for specifying which properties are affected by transitions.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transition-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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