// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class TransformOrigin : ClassDictionaryBase
{
    public TransformOrigin()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "origin-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        transform-origin: {0};
                        """,
                }
            },
            {
                "origin-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: center;
                        """,
                }
            },
            {
                "origin-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: top;
                        """,
                }
            },
            {
                "origin-top-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: top right;
                        """,
                }
            },
            {
                "origin-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: right;
                        """,
                }
            },
            {
                "origin-bottom-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: bottom right;
                        """,
                }
            },
            {
                "origin-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: bottom;
                        """,
                }
            },
            {
                "origin-bottom-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: bottom left;
                        """,
                }
            },
            {
                "origin-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: left;
                        """,
                }
            },
            {
                "origin-top-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-origin: top left;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}