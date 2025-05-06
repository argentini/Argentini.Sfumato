// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Svg;

public sealed class StrokeWidth : ClassDictionaryBase
{
    public StrokeWidth()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "stroke-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesDimensionLength = true,
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