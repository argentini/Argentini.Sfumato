// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Sepia : ClassDictionaryBase
{
    public Sepia()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "sepia-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAlphaNumber = true,
                    Template =
                        """
                        --sf-sepia: sepia({0}%);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-sepia: sepia({0});
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
            {
                "sepia", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-sepia: sepia(100%);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}