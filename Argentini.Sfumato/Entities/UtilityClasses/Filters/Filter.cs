// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Filter : ClassDictionaryBase
{
    public Filter()
    {
        Description = "Utilities for applying general filter effects to elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "filter-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        filter: {0};
                        """,
                }
            },
            {
                "filter-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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