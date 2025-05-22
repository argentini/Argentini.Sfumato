// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapType : ClassDictionaryBase
{
    public ScrollSnapType()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "snap-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-type: none;
                               """,
                }
            },
            {
                "snap-x", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-type: x var(--sf-scroll-snap-strictness);
                               """,
                }
            },
            {
                "snap-y", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-type: y var(--sf-scroll-snap-strictness);
                               """,
                }
            },
            {
                "snap-both", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-type: both var(--sf-scroll-snap-strictness);
                               """,
                }
            },
            {
                "snap-mandatory", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-scroll-snap-strictness: mandatory;
                               """,
                }
            },
            {
                "snap-proximity", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-scroll-snap-strictness: proximity;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}