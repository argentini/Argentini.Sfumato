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
                    Template =
                        """
                        --sf-gradient-from-position: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-from-position" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}