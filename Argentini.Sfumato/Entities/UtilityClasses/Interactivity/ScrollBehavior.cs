// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollBehavior : ClassDictionaryBase
{
    public ScrollBehavior()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scroll-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-behavior: auto;
                               """,
                }
            },
            {
                "scroll-smooth", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-behavior: smooth;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}