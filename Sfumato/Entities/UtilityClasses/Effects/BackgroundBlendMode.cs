// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BackgroundBlendMode : ClassDictionaryBase
{
    public BackgroundBlendMode()
    {
        Group = "background-blend-mode";
        Description = "Utilities for blending multiple background layers.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-blend-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: normal;
                        """,
                }
            },
            {
                "bg-blend-multiply", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: multiply;
                        """,
                }
            },
            {
                "bg-blend-screen", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: screen;
                        """,
                }
            },
            {
                "bg-blend-overlay", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: overlay;
                        """,
                }
            },
            {
                "bg-blend-darken", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: darken;
                        """,
                }
            },
            {
                "bg-blend-lighten", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: lighten;
                        """,
                }
            },
            {
                "bg-blend-color-dodge", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: color-dodge;
                        """,
                }
            },
            {
                "bg-blend-color-burn", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: color-burn;
                        """,
                }
            },
            {
                "bg-blend-hard-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: hard-light;
                        """,
                }
            },
            {
                "bg-blend-soft-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: soft-light;
                        """,
                }
            },
            {
                "bg-blend-difference", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: difference;
                        """,
                }
            },
            {
                "bg-blend-exclusion", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: exclusion;
                        """,
                }
            },
            {
                "bg-blend-hue", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: hue;
                        """,
                }
            },
            {
                "bg-blend-saturation", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: saturation;
                        """,
                }
            },
            {
                "bg-blend-color", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: color;
                        """,
                }
            },
            {
                "bg-blend-luminosity", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-blend-mode: luminosity;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
