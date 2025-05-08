// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Hyphens : ClassDictionaryBase
{
    public Hyphens()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "hyphens-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        hyphens: none;
                        """,
                }
            },
            {
                "hyphens-manual", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        hyphens: manual;
                        """,
                }
            },
            {
                "hyphens-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        hyphens: auto;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}