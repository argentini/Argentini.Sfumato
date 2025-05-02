// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MixBlendMode : ClassDictionaryBase
{
    public MixBlendMode()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mix-blend-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: normal;
                        """,
                }
            },
            {
                "mix-blend-multiply", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: multiply;
                        """,
                }
            },
            {
                "mix-blend-screen", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: screen;
                        """,
                }
            },
            {
                "mix-blend-overlay", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: overlay;
                        """,
                }
            },
            {
                "mix-blend-darken", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: darken;
                        """,
                }
            },
            {
                "mix-blend-lighten", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: lighten;
                        """,
                }
            },
            {
                "mix-blend-color-dodge", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: color-dodge;
                        """,
                }
            },
            {
                "mix-blend-color-burn", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: color-burn;
                        """,
                }
            },
            {
                "mix-blend-hard-light", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: hard-light;
                        """,
                }
            },
            {
                "mix-blend-soft-light", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: soft-light;
                        """,
                }
            },
            {
                "mix-blend-difference", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: difference;
                        """,
                }
            },
            {
                "mix-blend-exclusion", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: exclusion;
                        """,
                }
            },
            {
                "mix-blend-hue", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: hue;
                        """,
                }
            },
            {
                "mix-blend-saturation", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: saturation;
                        """,
                }
            },
            {
                "mix-blend-color", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: color;
                        """,
                }
            },
            {
                "mix-blend-luminosity", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: luminosity;
                        """,
                }
            },
            {
                "mix-blend-plus-darker", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: plus-darker;
                        """,
                }
            },
            {
                "mix-blend-plus-lighter", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mix-blend-mode: plus-lighter;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
