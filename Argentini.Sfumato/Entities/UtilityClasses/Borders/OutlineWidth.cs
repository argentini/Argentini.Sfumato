// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineWidth : ClassDictionaryBase
{
    public OutlineWidth()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = 
                        """
                        outline-width: {0}px;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
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
                        outline-width: 1px;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
