// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionBehavior : ClassDictionaryBase
{
    public TransitionBehavior()
    {
        Group = "transition-behavior";
        Description = "Utilities for configuring the overall behavior of transitions.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transition-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transition-behavior: normal;
                        """,
                }
            },
            {
                "transition-discrete", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transition-behavior: allow-discrete;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}