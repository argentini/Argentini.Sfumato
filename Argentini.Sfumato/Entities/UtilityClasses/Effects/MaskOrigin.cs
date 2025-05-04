// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskOrigin : ClassDictionaryBase
{
    public MaskOrigin()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-origin-border", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: border-box;
                        """,
                }
            },
            {
                "mask-origin-padding", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: padding-box;
                        """,
                }
            },
            {
                "mask-origin-content", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: content-box;
                        """,
                }
            },
            {
                "mask-origin-fill", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: fill-box;
                        """,
                }
            },
            {
                "mask-origin-stroke", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: stroke-box;
                        """,
                }
            },
            {
                "mask-origin-view", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-origin: view-box;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
