// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridAutoFlow : ClassDictionaryBase
{
    public GridAutoFlow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grid-flow-row", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-flow: row;
                        """,
                }
            },
            {
                "grid-flow-col", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-flow: column;
                        """,
                }
            },
            {
                "grid-flow-dense", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-flow: dense;
                        """,
                }
            },
            {
                "grid-flow-row-dense", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-flow: row dense;
                        """,
                }
            },
            {
                "grid-flow-col-dense", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-auto-flow: column dense;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}