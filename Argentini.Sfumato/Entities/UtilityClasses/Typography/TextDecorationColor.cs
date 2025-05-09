// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationColor : ClassDictionaryBase
{
    public TextDecorationColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "decoration-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               text-decoration-color: {0};
                               """,
                }
            },
            {
                "decoration-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-color: inherit;
                               """,
                }
            },
            {
                "decoration-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-decoration-color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}