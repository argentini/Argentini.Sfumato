// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ColorScheme : ClassDictionaryBase
{
    public ColorScheme()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scheme-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color-scheme: normal;
                               """,
                }
            },
            {
                "scheme-dark", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color-scheme: dark;
                               """,
                }
            },
            {
                "scheme-light", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color-scheme: light;
                               """,
                }
            },
            {
                "scheme-light-dark", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color-scheme: light dark;
                               """,
                }
            },
            {
                "scheme-only-dark", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color-scheme: only dark;
                               """,
                }
            },
            {
                "scheme-only-light", new ClassDefinition
                {
                    IsSimpleUtility = true,
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