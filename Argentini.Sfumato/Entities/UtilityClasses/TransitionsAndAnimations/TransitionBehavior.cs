// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.TransitionsAndAnimations;

public sealed class TransitionBehavior : ClassDictionaryBase
{
    public TransitionBehavior()
    {
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