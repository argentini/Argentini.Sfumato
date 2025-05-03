// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskClip : ClassDictionaryBase
{
    public MaskClip()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-clip-border", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: border-box;
                        """,
                }
            },
            {
                "mask-clip-padding", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: padding-box;
                        """,
                }
            },
            {
                "mask-clip-content", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: content-box;
                        """,
                }
            },
            {
                "mask-clip-fill", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: fill-box;
                        """,
                }
            },
            {
                "mask-clip-stroke", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: stroke-box;
                        """,
                }
            },
            {
                "mask-clip-view", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: view-box;
                        """,
                }
            },
            {
                "mask-no-clip", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-clip: no-clip;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
