// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class FieldSizing : ClassDictionaryBase
{
    public FieldSizing()
    {
        Group = "field-sizing";
        Description = "Utilities for controlling the sizing of form fields.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "field-sizing-fixed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               field-sizing: fixed;
                               """,
                }
            },
            {
                "field-sizing-content", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               field-sizing: content;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}