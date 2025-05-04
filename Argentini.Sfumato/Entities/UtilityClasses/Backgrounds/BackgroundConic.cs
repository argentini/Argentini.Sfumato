// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundConic : ClassDictionaryBase
{
    public BackgroundConic()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-conic-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesInteger = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: from {0}deg in oklab;
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: from {0}deg in {1};
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        --sf-gradient-position: {0};
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """, 
                }
            },
            {
                "-bg-conic-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesInteger = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: from -{0}deg in oklab;
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: from -{0}deg in {1};
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
            {
                "bg-conic", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: in oklab;
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: in {1};
                        background-image: conic-gradient(var(--sf-gradient-stops));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}