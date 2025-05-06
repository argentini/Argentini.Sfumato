// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAnimations;

public sealed class TransitionDelay : ClassDictionaryBase
{
    public TransitionDelay()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "delay-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesDurationTime = true,
                    UsesAbstractValue = true,
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