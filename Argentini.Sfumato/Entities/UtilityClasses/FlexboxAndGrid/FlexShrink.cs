// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexShrink : ClassDictionaryBase
{
    public FlexShrink()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shrink-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        flex-shrink: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-shrink: {0};
                        """,
                }
            },
            {
                "shrink", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-shrink: 1;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}