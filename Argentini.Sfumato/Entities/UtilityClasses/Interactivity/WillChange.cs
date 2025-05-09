// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class WillChange : ClassDictionaryBase
{
    public WillChange()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "will-change-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template = """
                               will-change: {0};
                               """,
                }
            },
            {
                "will-change-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               will-change: auto;
                               """,
                }
            },
            {
                "will-change-scroll", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               will-change: scroll-position;
                               """,
                }
            },
            {
                "will-change-contents", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               will-change: contents;
                               """,
                }
            },
            {
                "will-change-transform", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               will-change: transform;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}