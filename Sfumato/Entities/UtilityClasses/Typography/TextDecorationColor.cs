// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationColor : ClassDictionaryBase
{
    public TextDecorationColor()
    {
        Group = "text-decoration-color";
        Description = "Utilities for setting the color of text decorations.";
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