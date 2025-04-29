// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ViaPercentage : ClassDictionaryBase
{
    public ViaPercentage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "via-", new ClassDefinition
                {
                    UsesPercentage = true,
                    SelectorSort = 2,
                    Template =
                        """
                        --sf-gradient-via-position: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-via-position" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}