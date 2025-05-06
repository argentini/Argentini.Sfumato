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
                    UsesAbstractValue = true,
                    Template = """
                               will-change: {0};
                               """,
                }
            },
            {
                "will-change-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               will-change: auto;
                               """,
                }
            },
            {
                "will-change-scroll", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               will-change: scroll-position;
                               """,
                }
            },
            {
                "will-change-contents", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               will-change: contents;
                               """,
                }
            },
            {
                "will-change-transform", new ClassDefinition
                {
                    IsSimpleUtility = true,
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