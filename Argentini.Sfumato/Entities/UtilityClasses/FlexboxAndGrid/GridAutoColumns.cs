// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoColumns : ClassDictionaryBase
{
    public GridAutoColumns()
    {
        Group = "grid-auto-columns";
        Description = "Utilities for specifying the size of automatically created grid columns.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "auto-cols-", new ClassDefinition
                {
                    InFlexCollection = true,
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
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-columns: auto;
                        """,
                }
            },
            {
                "auto-cols-min", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-columns: min-content;
                        """,
                }
            },
            {
                "auto-cols-max", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-auto-columns: max-content;
                        """,
                }
            },
            {
                "auto-cols-fr", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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