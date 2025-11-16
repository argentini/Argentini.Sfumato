// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundConic : ClassDictionaryBase
{
    public BackgroundConic()
    {
        Group = "background-image";
        Description = "Utilities for applying conic gradient backgrounds to elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-conic-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    InAbstractValueCollection = true,
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
                    InAngleHueCollection = true,
                    InAbstractValueCollection = true,
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
                    InSimpleUtilityCollection = true,
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