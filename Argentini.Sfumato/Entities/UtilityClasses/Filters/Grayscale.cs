// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Grayscale : ClassDictionaryBase
{
    public Grayscale()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grayscale-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAlphaNumber = true,
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
                    IsSimpleUtility = true,
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