// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class TouchAction : ClassDictionaryBase
{
    public TouchAction()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "touch-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: auto;
                               """,
                }
            },
            {
                "touch-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: none;
                               """,
                }
            },
            {
                "touch-pan-x", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-x;
                               """,
                }
            },
            {
                "touch-pan-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-left;
                               """,
                }
            },
            {
                "touch-pan-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-right;
                               """,
                }
            },
            {
                "touch-pan-y", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-y;
                               """,
                }
            },
            {
                "touch-pan-up", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-up;
                               """,
                }
            },
            {
                "touch-pan-down", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pan-down;
                               """,
                }
            },
            {
                "touch-pinch-zoom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: pinch-zoom;
                               """,
                }
            },
            {
                "touch-manipulation", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               touch-action: manipulation;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}