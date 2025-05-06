// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Appearance : ClassDictionaryBase
{
    public Appearance()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "appearance-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               appearance: none;
                               """,
                }
            },
            {
                "appearance-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
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