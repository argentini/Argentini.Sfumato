// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskSize : ClassDictionaryBase
{
    public MaskSize()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-size-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        -webkit-mask-size: {0};
                        mask-size: {0};
                        """,
                }
            },
            {
                "mask-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-size: auto;
                        mask-size: auto;
                        """,
                }
            },
            {
                "mask-cover", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-size: cover;
                        mask-size: cover;
                        """,
                }
            },
            {
                "mask-contain", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        -webkit-mask-size: contain;
                        mask-size: contain;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
