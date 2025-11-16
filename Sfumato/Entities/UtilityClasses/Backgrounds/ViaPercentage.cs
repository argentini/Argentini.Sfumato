// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ViaPercentage : ClassDictionaryBase
{
    public ViaPercentage()
    {
        Group = "background-image";
        Description = "Utilities for specifying background gradient intermediate positions as percentages.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "via-", new ClassDefinition
                {
                    InPercentageCollection = true,
                    SelectorSort = 2,
                    Template =
                        """
                        --sf-gradient-via-position: {0};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}