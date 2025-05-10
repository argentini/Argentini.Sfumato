// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class Order : ClassDictionaryBase
{
    public Order()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "order-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    Template =
                        """
                        order: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        order: {0};
                        """,
                }
            },
            {
                "order-first", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        order: calc(-infinity);
                        """,
                }
            },
            {
                "order-last", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        order: calc(infinity);
                        """,
                }
            },
            {
                "order-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        order: 0;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}