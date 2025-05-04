// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskMode : ClassDictionaryBase
{
    public MaskMode()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-alpha", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-mode: alpha;
                        """,
                }
            },
            {
                "mask-luminance", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-mode: luminance;
                        """,
                }
            },
            {
                "mask-match", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-mode: match-source;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
