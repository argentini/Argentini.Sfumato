// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Tables;

public sealed class TableLayout : ClassDictionaryBase
{
    public TableLayout()
    {
        Group = "table-layout";
        Description = "Utilities for setting table layout algorithms.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "table-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        table-layout: auto;
                        """,
                }
            },
            {
                "table-fixed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        table-layout: fixed;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}