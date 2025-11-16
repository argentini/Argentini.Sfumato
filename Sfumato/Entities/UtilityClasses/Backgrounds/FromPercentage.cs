// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class FromPercentage : ClassDictionaryBase
{
    public FromPercentage()
    {
        Group = "background-image";
        Description = "Utilities for configuring background gradient from percentage.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "from-", new ClassDefinition
                {
                    InPercentageCollection = true,
                    SelectorSort = 1,
                    Template =
                        """
                        --sf-gradient-from-position: {0};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}