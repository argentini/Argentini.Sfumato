// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ToPercentage : ClassDictionaryBase
{
    public ToPercentage()
    {
        Group = "background-image";
        Description = "Utilities for configuring background gradient to percentage.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "to-", new ClassDefinition
                {
                    InPercentageCollection = true,
                    SelectorSort = 3,
                    Template =
                        """
                        --sf-gradient-to-position: {0};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}