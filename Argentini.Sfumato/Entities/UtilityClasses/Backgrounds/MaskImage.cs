// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class MaskImage : ClassDictionaryBase
{
    public MaskImage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesImageUrl = true,
                    Template = """
                               mask-image: {0};
                               """,
                }
            },
            {
                "mask-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               mask-image: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}