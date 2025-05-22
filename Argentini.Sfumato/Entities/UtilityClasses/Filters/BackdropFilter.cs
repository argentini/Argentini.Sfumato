// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class BackdropFilter : ClassDictionaryBase
{
    public BackdropFilter()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "backdrop-filter-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        backdrop-filter: {0};
                        """,
                }
            },
            {
                "backdrop-filter-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        backdrop-filter: none;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}