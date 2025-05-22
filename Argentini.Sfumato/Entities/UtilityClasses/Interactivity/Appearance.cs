// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Appearance : ClassDictionaryBase
{
    public Appearance()
    {
        Description = "Utilities for toggling native UI appearance.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "appearance-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               appearance: none;
                               """,
                }
            },
            {
                "appearance-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               appearance: auto;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}