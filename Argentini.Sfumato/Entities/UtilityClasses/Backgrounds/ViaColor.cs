// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ViaColor : ClassDictionaryBase
{
    public ViaColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "via-", new ClassDefinition
                {
                    UsesColor = true,
                    Template =
                        """
                        --sf-gradient-via: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-via" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}