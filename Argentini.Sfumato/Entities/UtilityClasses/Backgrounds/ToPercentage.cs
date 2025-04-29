// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ToPercentage : ClassDictionaryBase
{
    public ToPercentage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "to-", new ClassDefinition
                {
                    UsesPercentage = true,
                    SelectorSort = 3,
                    Template =
                        """
                        --sf-gradient-to-position: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-to-position" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}