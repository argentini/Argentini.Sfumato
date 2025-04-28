// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class ToColor : ClassDictionaryBase
{
    public ToColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "to-", new ClassDefinition
                {
                    UsesColor = true,
                    Template =
                        """
                        --sf-gradient-to: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-to" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}