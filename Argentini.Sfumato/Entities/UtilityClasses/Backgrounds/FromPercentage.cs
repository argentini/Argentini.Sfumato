// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class FromPercentage : ClassDictionaryBase
{
    public FromPercentage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "from-", new ClassDefinition
                {
                    UsesPercentage = true,
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