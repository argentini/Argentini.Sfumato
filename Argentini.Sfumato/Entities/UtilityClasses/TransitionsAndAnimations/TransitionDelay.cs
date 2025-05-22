// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionDelay : ClassDictionaryBase
{
    public TransitionDelay()
    {
        Description = "Utilities for defining delays before transitions start.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "delay-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InDurationCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        transition-delay: {0}ms;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        transition-delay: {0};
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}