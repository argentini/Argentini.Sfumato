// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridTemplateColumns : ClassDictionaryBase
{
    public GridTemplateColumns()
    {
        Group = "grid-template-columns";
        Description = "Utilities for defining the number and width of grid columns.";
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
                "grid-cols-0", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-template-columns: 0fr;
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