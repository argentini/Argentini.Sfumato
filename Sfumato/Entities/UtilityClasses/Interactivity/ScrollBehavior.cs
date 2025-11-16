// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollBehavior : ClassDictionaryBase
{
    public ScrollBehavior()
    {
        Group = "scroll-behavior";
        Description = "Utilities for controlling scroll behavior.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scroll-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-behavior: auto;
                               """,
                }
            },
            {
                "scroll-smooth", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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