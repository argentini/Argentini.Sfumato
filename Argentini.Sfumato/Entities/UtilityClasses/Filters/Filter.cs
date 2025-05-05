// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Filter : ClassDictionaryBase
{
    public Filter()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "filter-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        filter: {0};
                        """,
                }
            },
            {
                "filter-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        filter: none;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}