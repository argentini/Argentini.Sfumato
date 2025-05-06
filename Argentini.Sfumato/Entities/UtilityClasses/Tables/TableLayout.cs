// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Tables;

public sealed class TableLayout : ClassDictionaryBase
{
    public TableLayout()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "table-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        table-layout: auto;
                        """,
                }
            },
            {
                "table-fixed", new ClassDefinition
                {
                    IsSimpleUtility = true,
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