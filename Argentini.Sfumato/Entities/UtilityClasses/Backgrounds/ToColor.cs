// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ToColor : ClassDictionaryBase
{
    public ToColor()
    {
        Description = "Utilities for configuring tocolor.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "to-", new ClassDefinition
                {
                    InColorCollection = true,
                    SelectorSort = 3,
                    Template =
                        """
                        --sf-gradient-to: {0};
                        --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position))
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}