// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class BackdropFilter : ClassDictionaryBase
{
    public BackdropFilter()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "backdrop-filter-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        backdrop-filter: {0};
                        """,
                }
            },
            {
                "backdrop-filter-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
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