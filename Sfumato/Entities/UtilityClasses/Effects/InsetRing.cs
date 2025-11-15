// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetRing : ClassDictionaryBase
{
    public InsetRing()
    {
        Group = "box-shadow";
        Description = "Utilities for creating inset rings inside elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-ring-", new ClassDefinition
                {
                    InLengthCollection = true,
                    InAbstractValueCollection = true,
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
                }
            },
            {
                "inset-ring", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        --sf-ring-inset: inset;
                        --sf-inset-ring-shadow: inset 0 0 0 1px var(--sf-inset-ring-color, currentcolor);
                        box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
