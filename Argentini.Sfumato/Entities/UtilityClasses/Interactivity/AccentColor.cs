// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class AccentColor : ClassDictionaryBase
{
    public AccentColor()
    {
        Group = "accent-color";
        Description = "Utilities for customizing accent color for user interface controls.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "accent-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               accent-color: {0};
                               """,
                }
            },
            {
                "accent-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               accent-color: inherit;
                               """,
                }
            },
            {
                "accent-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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