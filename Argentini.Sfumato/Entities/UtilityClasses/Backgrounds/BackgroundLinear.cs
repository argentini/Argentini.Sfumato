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
                    Template =
                        """
                        @supports (background-image:linear-gradient(in lab, red, red)) {
                            --sf-gradient-position: {0}deg in oklab;
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
                    Template = """
                               @supports (background-image:linear-gradient(in lab, red, red)) {
                                   --sf-gradient-position: -{0}deg in oklab;
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
                    Template = """
                               background-image: background-image: linear-gradient(to top, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-tr", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to top right, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-r", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to right, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-br", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-b", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to bottom, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-bl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-l", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to left, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
            {
                "bg-linear-to-tl", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-image: background-image: linear-gradient(to top left, var(--sf-gradient-stops));
                               """,
                    UsesCssCustomProperties = [ "--sf-gradient-stops" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}