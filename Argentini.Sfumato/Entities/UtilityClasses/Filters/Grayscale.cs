// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Grayscale : ClassDictionaryBase
{
    public Grayscale()
    {
        Group = "filter/grayscale";
        Description = "Utilities for rendering elements in grayscale.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grayscale-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    Template =
                        """
                        --sf-grayscale: grayscale({0}%);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-grayscale: grayscale({0});
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
            {
                "grayscale", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-grayscale: grayscale(100%);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}