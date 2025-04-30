// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoRows : ClassDictionaryBase
{
    public GridAutoRows()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "auto-rows-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-auto-rows: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-auto-rows: {0};
                        """,
                }
            },
            {
                "auto-rows-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-rows: auto;
                        """,
                }
            },
            {
                "auto-rows-min", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-rows: min-content;
                        """,
                }
            },
            {
                "auto-rows-max", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-rows: max-content;
                        """,
                }
            },
            {
                "auto-rows-fr", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-rows: minmax(0, 1fr);
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}