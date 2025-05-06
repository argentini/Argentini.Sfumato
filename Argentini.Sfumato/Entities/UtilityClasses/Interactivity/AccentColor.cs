// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class AccentColor : ClassDictionaryBase
{
    public AccentColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "accent-", new ClassDefinition
                {
                    UsesColor = true,
                    Template = """
                               accent-color: {0};
                               """,
                }
            },
            {
                "accent-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               accent-color: inherit;
                               """,
                }
            },
            {
                "accent-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               accent-color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}