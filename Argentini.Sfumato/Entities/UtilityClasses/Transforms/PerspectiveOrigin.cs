// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class PerspectiveOrigin : ClassDictionaryBase
{
    public PerspectiveOrigin()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "perspective-origin-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        perspective-origin: {0};
                        """,
                }
            },
            {
                "perspective-origin-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: center;
                        """,
                }
            },
            {
                "perspective-origin-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: top;
                        """,
                }
            },
            {
                "perspective-origin-top-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: top right;
                        """,
                }
            },
            {
                "perspective-origin-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: right;
                        """,
                }
            },
            {
                "perspective-origin-bottom-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: bottom right;
                        """,
                }
            },
            {
                "perspective-origin-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: bottom;
                        """,
                }
            },
            {
                "perspective-origin-bottom-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: bottom left;
                        """,
                }
            },
            {
                "perspective-origin-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: left;
                        """,
                }
            },
            {
                "perspective-origin-top-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        perspective-origin: top left;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}