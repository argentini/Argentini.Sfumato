// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Svg;

public sealed class Fill : ClassDictionaryBase
{
    public Fill()
    {
        Group = "fill";
        Description = "Utilities for setting SVG fill color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "fill-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               fill: {0};
                               """,
                }
            },
            {
                "fill-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               fill: inherit;
                               """,
                }
            },
            {
                "fill-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               fill: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}