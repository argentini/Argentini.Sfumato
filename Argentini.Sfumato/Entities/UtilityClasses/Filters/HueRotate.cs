// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class HueRotate : ClassDictionaryBase
{
    public HueRotate()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "hue-rotate-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-hue-rotate: hue-rotate({0}deg);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-hue-rotate: hue-rotate({0});
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}