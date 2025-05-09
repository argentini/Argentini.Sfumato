// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

public sealed class MaskImage : ClassDictionaryBase
{
    public MaskImage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InUrlCollection = true,
                    Template = """
                               -webkit-mask-image: {0};
                               mask-image: {0};
                               """,
                }
            },
            {
                "mask-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               -webkit-mask-image: none;
                               mask-image: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}