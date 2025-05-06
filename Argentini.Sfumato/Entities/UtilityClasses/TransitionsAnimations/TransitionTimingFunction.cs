// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAnimations;

public sealed class TransitionTimingFunction : ClassDictionaryBase
{
    public TransitionTimingFunction()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "ease-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesDurationTime = true,
                    Template =
                        """
                        --sf-ease: {0};
                        transition-timing-function: {0};
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-ease: {0};
                        transition-timing-function: {0};
                        """,
                }
            },
            {
                "ease-linear", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-ease: linear;
                        transition-timing-function: linear;
                        """,
                }
            },
            {
                "ease-in", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-ease: var(--ease-in);
                        transition-timing-function: var(--ease-in);
                        """,
                }
            },
            {
                "ease-out", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-ease: var(--ease-out);
                        transition-timing-function: var(--ease-out);
                        """,
                }
            },
            {
                "ease-in-out", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-ease: var(--ease-in-out);
                        transition-timing-function: var(--ease-in-out);
                        """,
                }
            },
            {
                "ease-initial", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-ease: initial;
                        transition-timing-function: initial;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}