// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Accessibility;

public sealed class ForcedColorAdjust : ClassDictionaryBase
{
    public ForcedColorAdjust()
    {
        Group = "forced-color-adjust";
        Description = "Utilities for controlling forced color mode adjustments for accessibility.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "forced-color-adjust-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        forced-color-adjust: auto;
                        """,
                }
            },
            {
                "forced-color-adjust-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        forced-color-adjust: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}