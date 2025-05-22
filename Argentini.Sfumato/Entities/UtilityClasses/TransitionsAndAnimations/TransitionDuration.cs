// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionDuration : ClassDictionaryBase
{
    public TransitionDuration()
    {
        Group = "transition-duration";
        Description = "Utilities for setting transition duration.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "duration-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InDurationCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        --sf-duration: {0}ms;
                        transition-duration: {0}ms;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-duration: {0};
                        transition-duration: {0};
                        """,
                }
            },
            {
                "duration-initial", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-duration: initial;
                        transition-duration: initial;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}