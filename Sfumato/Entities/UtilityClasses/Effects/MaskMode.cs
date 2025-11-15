// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskMode : ClassDictionaryBase
{
    public MaskMode()
    {
        Group = "mask-mode";
        Description = "Utilities for configuring mask mode.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-alpha", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-source-type: alpha;
                        mask-mode: alpha;
                        """,
                }
            },
            {
                "mask-luminance", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-source-type: luminance;
                        mask-mode: luminance;
                        """,
                }
            },
            {
                "mask-match", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-source-type: match-source;
                        mask-mode: match-source;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
