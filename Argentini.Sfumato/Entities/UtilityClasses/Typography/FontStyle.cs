// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontStyle : ClassDictionaryBase
{
    public FontStyle()
    {
        Group = "font-style";
        Description = "Utilities for setting font style.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "italic", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               font-style: italic;
                               """
                }
            },
            {
                "not-italic", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               font-style: normal;
                               """
                }
            }
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}