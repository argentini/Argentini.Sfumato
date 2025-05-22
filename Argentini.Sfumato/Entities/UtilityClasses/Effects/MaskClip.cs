// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskClip : ClassDictionaryBase
{
    public MaskClip()
    {
        Group = "mask-clip";
        Description = "Utilities for controlling the mask clipping area.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-clip-border", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: border-box;
                        mask-clip: border-box;
                        """,
                }
            },
            {
                "mask-clip-padding", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: padding-box;
                        mask-clip: padding-box;
                        """,
                }
            },
            {
                "mask-clip-content", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: content-box;
                        mask-clip: content-box;
                        """,
                }
            },
            {
                "mask-clip-fill", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: fill-box;
                        mask-clip: fill-box;
                        """,
                }
            },
            {
                "mask-clip-stroke", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: stroke-box;
                        mask-clip: stroke-box;
                        """,
                }
            },
            {
                "mask-clip-view", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: view-box;
                        mask-clip: view-box;
                        """,
                }
            },
            {
                "mask-no-clip", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-clip: no-clip;
                        mask-clip: no-clip;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
