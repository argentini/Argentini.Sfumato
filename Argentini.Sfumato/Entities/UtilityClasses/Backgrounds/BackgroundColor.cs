// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundColor : ClassDictionaryBase
{
    public BackgroundColor()
    {
        Description = "Utilities for setting background color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               background-color: {0};
                               """,
                }
            },
            {
                "bg-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-color: inherit;
                               """,
                }
            },
            {
                "bg-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}