// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class Flex : ClassDictionaryBase
{
    public Flex()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "flex-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InLengthCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        flex: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex: {0};
                        """,
                }
            },
            {
                "flex-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex: 1 1 auto;
                        """,
                }
            },
            {
                "flex-initial", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex: 0 1 auto;
                        """,
                }
            },
            {
                "flex-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex: none;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}