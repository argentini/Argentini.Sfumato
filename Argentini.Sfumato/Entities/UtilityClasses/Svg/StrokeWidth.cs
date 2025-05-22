// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Svg;

public sealed class StrokeWidth : ClassDictionaryBase
{
    public StrokeWidth()
    {
        Group = "stroke";
        Description = "Utilities for setting SVG stroke width.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "stroke-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               stroke-width: {0}px;
                               """,
                    ArbitraryCssValueTemplate = """
                               stroke-width: {0};
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}