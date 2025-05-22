// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineWidth : ClassDictionaryBase
{
    public OutlineWidth()
    {
        Description = "Utilities for setting outline width.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = 
                        """
                        outline-style: var(--sf-outline-style);
                        outline-width: {0}px;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        outline-style: var(--sf-outline-style);
                        outline-width: {0};
                        """,
                }
            },
            {
                "outline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        outline-style: var(--sf-outline-style);
                        outline-width: 1px;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
