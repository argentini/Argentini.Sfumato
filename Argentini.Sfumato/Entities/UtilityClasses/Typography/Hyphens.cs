// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Hyphens : ClassDictionaryBase
{
    public Hyphens()
    {
        Group = "hyphens";
        Description = "Utilities for enabling or disabling automatic hyphenation.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "hyphens-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        hyphens: none;
                        """,
                }
            },
            {
                "hyphens-manual", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        hyphens: manual;
                        """,
                }
            },
            {
                "hyphens-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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