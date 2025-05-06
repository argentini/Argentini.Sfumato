// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAnimations;

public sealed class TransitionBehavior : ClassDictionaryBase
{
    public TransitionBehavior()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transition-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transition-behavior: normal;
                        """,
                }
            },
            {
                "transition-discrete", new ClassDefinition
                {
                    IsSimpleUtility = true,
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