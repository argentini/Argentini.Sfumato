// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapAlign : ClassDictionaryBase
{
    public ScrollSnapAlign()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "snap-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-align: start;
                               """,
                }
            },
            {
                "snap-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-align: end;
                               """,
                }
            },
            {
                "snap-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-align: center;
                               """,
                }
            },
            {
                "snap-align-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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