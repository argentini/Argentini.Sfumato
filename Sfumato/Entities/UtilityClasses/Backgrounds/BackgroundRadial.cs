// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundRadial : ClassDictionaryBase
{
    public BackgroundRadial()
    {
        Group = "background-image";
        Description = "Utilities for applying radial gradient backgrounds.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-radial-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    ArbitraryCssValueTemplate =
                        """
                        --sf-gradient-position: {0};
                        background-image: radial-gradient(var(--sf-gradient-stops, {0}));
                        """, 
                }
            },
            {
                "bg-radial", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-gradient-position: in oklab;
                        background-image: radial-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}