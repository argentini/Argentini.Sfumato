// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoRows : ClassDictionaryBase
{
    public GridAutoRows()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "auto-rows-", new ClassDefinition
                {
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
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
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-rows: auto;
                        """,
                }
            },
            {
                "auto-rows-min", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-rows: min-content;
                        """,
                }
            },
            {
                "auto-rows-max", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-rows: max-content;
                        """,
                }
            },
            {
                "auto-rows-fr", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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