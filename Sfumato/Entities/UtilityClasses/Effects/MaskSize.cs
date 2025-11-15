// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskSize : ClassDictionaryBase
{
    public MaskSize()
    {
        Group = "mask-size";
        Description = "Utilities for defining the size of masks.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-size-", new ClassDefinition
                {
                    SelectorSort = 4,
                    InAbstractValueCollection = true,
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
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
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
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
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
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
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
