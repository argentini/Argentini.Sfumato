// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Ring : ClassDictionaryBase
{
    public Ring()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "ring-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        --sf-ring-shadow: var(--sf-ring-inset, ) 0 0 0 calc({0}px + var(--sf-ring-offset-width)) var(--sf-ring-color, currentcolor);
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-ring-shadow: {0};
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                }
            },
            {
                "ring", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-ring-shadow: var(--sf-ring-inset, ) 0 0 0 calc(1px + var(--sf-ring-offset-width)) var(--sf-ring-color, currentcolor);
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
