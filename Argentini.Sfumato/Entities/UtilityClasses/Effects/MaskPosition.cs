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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
                    SelectorSort = 4,
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
