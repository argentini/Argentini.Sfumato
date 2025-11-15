// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapAlign : ClassDictionaryBase
{
    public ScrollSnapAlign()
    {
        Group = "scroll-snap-align";
        Description = "Utilities for aligning elements to snap points during scrolling.";
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