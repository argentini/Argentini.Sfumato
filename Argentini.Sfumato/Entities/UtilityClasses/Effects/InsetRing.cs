// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetRing : ClassDictionaryBase
{
    public InsetRing()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-ring-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        --sf-ring-inset: inset;
                        --sf-inset-ring-shadow: inset 0 0 0 {0}px var(--sf-inset-ring-color, currentcolor);
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-ring-inset: inset;
                        --sf-inset-ring-shadow: {0};
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                    UsesCssCustomProperties = [ "--sf-ring-inset", "--sf-inset-ring-shadow", "--sf-inset-ring-color", "--sf-inset-shadow", "--sf-ring-offset-shadow", "--sf-ring-shadow", "--sf-shadow" ]
                }
            },
            {
                "inset-ring", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-ring-inset: inset;
                        --sf-inset-ring-shadow: inset 0 0 0 1px var(--sf-inset-ring-color, currentcolor);
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                    UsesCssCustomProperties = [ "--sf-ring-inset", "--sf-inset-ring-shadow", "--sf-inset-ring-color", "--sf-inset-shadow", "--sf-ring-offset-shadow", "--sf-ring-shadow", "--sf-shadow" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
