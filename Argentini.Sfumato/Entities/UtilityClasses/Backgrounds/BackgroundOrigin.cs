// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundOrigin : ClassDictionaryBase
{
    public BackgroundOrigin()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-origin-border", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-origin: border-box;
                               """,
                }
            },
            {
                "bg-origin-padding", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-origin: padding-box;
                               """,
                }
            },
            {
                "bg-origin-content", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-origin: content-box;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}