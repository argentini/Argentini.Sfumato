// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundLinear : ClassDictionaryBase
{
    public BackgroundLinear()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-linear-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesInteger = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in oklab;
                        }

                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sw-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in {1};
                        }

                        --sf-gradient-position: {0}deg;
                        background-image: linear-gradient(var(--sw-gradient-stops));
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        background-image: linear-gradient({0});
                        """, 
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "-bg-linear-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in oklab;
                        }

                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sw-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: -{0}deg in {1};
                        }

                        --sf-gradient-position: -{0}deg;
                        background-image: linear-gradient(var(--sw-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-t", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to top, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top in {1};
                        }
                        
                        --sf-gradient-position: to top;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-tr", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to top right, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top right in {1};
                        }

                        --sf-gradient-position: to top right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-r", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to right, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to right in {1};
                        }

                        --sf-gradient-position: to right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-br", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom right in {1};
                        }

                        --sf-gradient-position: to bottom right;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-b", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to bottom, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom in {1};
                        }

                        --sf-gradient-position: to bottom;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-bl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to bottom left in {1};
                        }

                        --sf-gradient-position: to bottom left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-l", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to left, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to left in {1};
                        }

                        --sf-gradient-position: to left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
            {
                "bg-linear-to-tl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        background-image: background-image: linear-gradient(to top left, var(--sf-gradient-stops));
                        """,
                    ModifierTemplate = 
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: to top left in {1};
                        }

                        --sf-gradient-position: to top left;
                        background-image: linear-gradient(var(--sf-gradient-stops));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops", "--sf-gradient-position" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}