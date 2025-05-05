// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskType : ClassDictionaryBase
{
    public MaskType()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-type-alpha", new ClassDefinition
                {
                    SelectorSort = 4,
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
