// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Svg;

public sealed class Stroke : ClassDictionaryBase
{
    public Stroke()
    {
        Group = "stroke";
        GroupDescription = "Utilities for setting SVG stroke attributes.";
        Description = "Utilities for setting SVG stroke color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "stroke-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               stroke: {0};
                               """,
                }
            },
            {
                "stroke-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               stroke: inherit;
                               """,
                }
            },
            {
                "stroke-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               stroke: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}