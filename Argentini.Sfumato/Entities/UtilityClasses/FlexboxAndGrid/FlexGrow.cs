// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexGrow : ClassDictionaryBase
{
    public FlexGrow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grow-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        flex-grow: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-grow: {0};
                        """,
                }
            },
            {
                "grow", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-grow: 1;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}