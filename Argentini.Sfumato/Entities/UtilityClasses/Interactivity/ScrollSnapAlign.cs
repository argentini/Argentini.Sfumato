// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapAlign : ClassDictionaryBase
{
    public ScrollSnapAlign()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "snap-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-align: start;
                               """,
                }
            },
            {
                "snap-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-align: end;
                               """,
                }
            },
            {
                "snap-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-align: center;
                               """,
                }
            },
            {
                "snap-align-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-align: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}