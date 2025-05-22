// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskOrigin : ClassDictionaryBase
{
    public MaskOrigin()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-origin-border", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: border-box;
                        mask-origin: border-box;
                        """,
                }
            },
            {
                "mask-origin-padding", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: padding-box;
                        mask-origin: padding-box;
                        """,
                }
            },
            {
                "mask-origin-content", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: content-box;
                        mask-origin: content-box;
                        """,
                }
            },
            {
                "mask-origin-fill", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: fill-box;
                        mask-origin: fill-box;
                        """,
                }
            },
            {
                "mask-origin-stroke", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: stroke-box;
                        mask-origin: stroke-box;
                        """,
                }
            },
            {
                "mask-origin-view", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-origin: view-box;
                        mask-origin: view-box;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
