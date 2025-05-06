// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Svg;

public sealed class Fill : ClassDictionaryBase
{
    public Fill()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "fill-", new ClassDefinition
                {
                    UsesColor = true,
                    Template = """
                               fill: {0};
                               """,
                }
            },
            {
                "fill-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               fill: inherit;
                               """,
                }
            },
            {
                "fill-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
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