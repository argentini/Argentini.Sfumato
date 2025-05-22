// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ColorScheme : ClassDictionaryBase
{
    public ColorScheme()
    {
        Group = "color-scheme";
        Description = "Utilities for configuring colorscheme.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scheme-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: normal;
                               """,
                }
            },
            {
                "scheme-dark", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: dark;
                               """,
                }
            },
            {
                "scheme-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: light;
                               """,
                }
            },
            {
                "scheme-light-dark", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: light dark;
                               """,
                }
            },
            {
                "scheme-only-dark", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: only dark;
                               """,
                }
            },
            {
                "scheme-only-light", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color-scheme: only light;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}