// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class Color : ClassDictionaryBase
{
    public Color()
    {
        Group = "color";
        Description = "Utilities for setting text color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               color: {0};
                               """,
                }
            },
            {
                "text-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color: inherit;
                               """,
                }
            },
            {
                "text-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}