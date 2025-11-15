// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskType : ClassDictionaryBase
{
    public MaskType()
    {
        Group = "mask-type";
        Description = "Utilities for specifying the mask rendering mode.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-type-alpha", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mask-type: alpha;
                        """,
                }
            },
            {
                "mask-type-luminance", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        mask-type: luminance;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
