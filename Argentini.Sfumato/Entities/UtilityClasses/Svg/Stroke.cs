// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Svg;

public sealed class Stroke : ClassDictionaryBase
{
    public Stroke()
    {
        Description = "";
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