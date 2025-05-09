// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class Saturate : ClassDictionaryBase
{
    public Saturate()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "saturate-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAlphaNumber = true,
                    Template =
                        """
                        --sf-saturate: saturate({0}%);
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-saturate: saturate({0});
                        filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}