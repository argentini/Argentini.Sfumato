// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class TouchAction : ClassDictionaryBase
{
    public TouchAction()
    {
        Group = "touch-action";
        Description = "Utilities for specifying touch interaction behavior.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "touch-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               touch-action: auto;
                               """,
                }
            },
            {
                "touch-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               touch-action: none;
                               """,
                }
            },
            {
                "touch-pan-x", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-x: pan-x;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pan-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-x: pan-left;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pan-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-x: pan-right;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pan-y", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-y: pan-y;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pan-up", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-y: pan-up;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pan-down", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pan-y: pan-down;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-pinch-zoom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-pinch-zoom: pinch-zoom;
                               touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                               """,
                }
            },
            {
                "touch-manipulation", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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