// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskPosition : ClassDictionaryBase
{
    public MaskPosition()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-position-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        -webkit-mask-position: {0};
                        mask-position: {0};
                        """,
                }
            },
            {
                "mask-top-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: top left;
                        mask-position: top left;
                        """,
                }
            },
            {
                "mask-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: top;
                        mask-position: top;
                        """,
                }
            },
            {
                "mask-top-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: top right;
                        mask-position: top right;
                        """,
                }
            },
            {
                "mask-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: left;
                        mask-position: left;
                        """,
                }
            },
            {
                "mask-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: center;
                        mask-position: center;
                        """,
                }
            },
            {
                "mask-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: right;
                        mask-position: right;
                        """,
                }
            },
            {
                "mask-bottom-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: bottom left;
                        mask-position: bottom left;
                        """,
                }
            },
            {
                "mask-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: bottom;
                        mask-position: bottom;
                        """,
                }
            },
            {
                "mask-bottom-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-position: bottom right;
                        mask-position: bottom right;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
