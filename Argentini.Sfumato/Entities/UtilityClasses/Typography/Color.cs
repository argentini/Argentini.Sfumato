// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Color : ClassDictionaryBase
{
    public Color()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    UsesColor = true,
                    Template = """
                               color: {0};
                               """,
                }
            },
            {
                "text-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               color: inherit;
                               """,
                }
            },
            {
                "text-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
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