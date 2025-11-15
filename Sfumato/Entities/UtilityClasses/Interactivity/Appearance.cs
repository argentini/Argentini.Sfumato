// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Appearance : ClassDictionaryBase
{
    public Appearance()
    {
        Group = "appearance";
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