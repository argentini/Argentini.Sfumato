// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class FromColor : ClassDictionaryBase
{
    public FromColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "from-", new ClassDefinition
                {
                    UsesColor = true,
                    Template =
                        """
                        --sf-gradient-from: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-from" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}