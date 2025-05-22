// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundImage : ClassDictionaryBase
{
    public BackgroundImage()
    {
        Description = "Utilities for setting background images.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-", new ClassDefinition
                {
                    InUrlCollection = true,
                    Template = """
                               background-image: {0};
                               """,
                }
            },
            {
                "bg-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-image: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}