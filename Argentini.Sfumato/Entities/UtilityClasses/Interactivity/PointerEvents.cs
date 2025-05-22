// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class PointerEvents : ClassDictionaryBase
{
    public PointerEvents()
    {
        Group = "pointer-events";
        Description = "Utilities for enabling or disabling pointer events.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "pointer-events-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               pointer-events: auto;
                               """,
                }
            },
            {
                "pointer-events-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               pointer-events: none;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}