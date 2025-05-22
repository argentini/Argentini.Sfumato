// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ToPercentage : ClassDictionaryBase
{
    public ToPercentage()
    {
        Description = "";
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