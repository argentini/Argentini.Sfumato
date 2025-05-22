// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ViaColor : ClassDictionaryBase
{
    public ViaColor()
    {
        Group = "background-image";
        Description = "Utilities for configuring background gradient via color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "via-", new ClassDefinition
                {
                    InColorCollection = true,
                    SelectorSort = 2,
                    Template =
                        """
                        --sf-gradient-via: {0};
                        --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                        --sf-gradient-stops: var(--sf-gradient-via-stops);
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}