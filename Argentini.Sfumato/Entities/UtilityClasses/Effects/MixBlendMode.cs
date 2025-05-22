// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MixBlendMode : ClassDictionaryBase
{
    public MixBlendMode()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mix-blend-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: normal;
                        """,
                }
            },
            {
                "mix-blend-multiply", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: multiply;
                        """,
                }
            },
            {
                "mix-blend-screen", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: screen;
                        """,
                }
            },
            {
                "mix-blend-overlay", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: overlay;
                        """,
                }
            },
            {
                "mix-blend-darken", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: darken;
                        """,
                }
            },
            {
                "mix-blend-lighten", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: lighten;
                        """,
                }
            },
            {
                "mix-blend-color-dodge", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: color-dodge;
                        """,
                }
            },
            {
                "mix-blend-color-burn", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: color-burn;
                        """,
                }
            },
            {
                "mix-blend-hard-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: hard-light;
                        """,
                }
            },
            {
                "mix-blend-soft-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: soft-light;
                        """,
                }
            },
            {
                "mix-blend-difference", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: difference;
                        """,
                }
            },
            {
                "mix-blend-exclusion", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: exclusion;
                        """,
                }
            },
            {
                "mix-blend-hue", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: hue;
                        """,
                }
            },
            {
                "mix-blend-saturation", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: saturation;
                        """,
                }
            },
            {
                "mix-blend-color", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: color;
                        """,
                }
            },
            {
                "mix-blend-luminosity", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: luminosity;
                        """,
                }
            },
            {
                "mix-blend-plus-darker", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mix-blend-mode: plus-darker;
                        """,
                }
            },
            {
                "mix-blend-plus-lighter", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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
