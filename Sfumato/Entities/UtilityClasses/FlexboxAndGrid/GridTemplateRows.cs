// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class GridTemplateRows : ClassDictionaryBase
{
    public GridTemplateRows()
    {
        Group = "grid-template-rows";
        Description = "Utilities for defining the number and height of grid rows.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grid-rows-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
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
                "grid-rows-0", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-template-rows: 0fr;
                        """,
                }
            },
            {
                "grid-rows-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        grid-template-rows: none;
                        """,
                }
            },
            {
                "grid-rows-subgrid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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