// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundRadial : ClassDictionaryBase
{
    public BackgroundRadial()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-radial-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    ArbitraryCssValueTemplate =
                        """
                        --sf-gradient-position: {0};
                        background-image: radial-gradient(var(--sf-gradient-stops, {0}));
                        """, 
                    UsesCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
                }
            },
            {
                "bg-radial", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        --sf-gradient-position: in oklab;
                        background-image: radial-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}