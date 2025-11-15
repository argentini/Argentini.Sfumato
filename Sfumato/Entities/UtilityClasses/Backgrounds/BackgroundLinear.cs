// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundLinear : ClassDictionaryBase
{
    public BackgroundLinear()
    {
        Group = "background-image";
        Description = "Utilities for applying linear gradient backgrounds.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    InAbstractValueCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in {1};
                        }
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        background-image: linear-gradient(var(--sf-gradient-stops, {0}))
                        """, 
                }
            },
            {
                "-bg-linear-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-t", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to top;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to top;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-tr", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to top right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top right in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to top right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top right in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-r", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to right in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to right in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-br", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to bottom right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom right in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to bottom right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom right in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-b", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to bottom;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to bottom;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-bl", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to bottom left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom left in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to bottom left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom left in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-l", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to left in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to left in {1};
                        }
                        """,
                }
            },
            {
                "bg-linear-to-tl", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        --sf-gradient-position: to top left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top left in oklab;
                        }
                        """,
                    ModifierTemplate = 
                        """
                        --sf-gradient-position: to top left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top left in {1};
                        }
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}