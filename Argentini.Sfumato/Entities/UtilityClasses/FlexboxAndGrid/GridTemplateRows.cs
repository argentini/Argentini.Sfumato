// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridTemplateRows : ClassDictionaryBase
{
    public GridTemplateRows()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grid-rows-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        grid-template-rows: repeat({0}, minmax(0, 1fr));
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-template-rows: {0};
                        """,
                }
            },
            {
                "grid-rows-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-template-rows: none;
                        """,
                }
            },
            {
                "grid-rows-subgrid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        grid-template-rows: subgrid;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}