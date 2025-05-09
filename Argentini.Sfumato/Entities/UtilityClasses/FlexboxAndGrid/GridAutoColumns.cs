// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoColumns : ClassDictionaryBase
{
    public GridAutoColumns()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "auto-cols-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        grid-auto-columns: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-auto-columns: {0};
                        """,
                }
            },
            {
                "auto-cols-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-columns: auto;
                        """,
                }
            },
            {
                "auto-cols-min", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-columns: min-content;
                        """,
                }
            },
            {
                "auto-cols-max", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-columns: max-content;
                        """,
                }
            },
            {
                "auto-cols-fr", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-columns: minmax(0, 1fr);
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}