// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridTemplateColumns : ClassDictionaryBase
{
    public GridTemplateColumns()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grid-cols-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
                    Template =
                        """
                        grid-template-columns: repeat({0}, minmax(0, 1fr));
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        grid-template-columns: {0};
                        """,
                }
            },
            {
                "grid-cols-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-template-columns: none;
                        """,
                }
            },
            {
                "grid-cols-subgrid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-template-columns: subgrid;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}