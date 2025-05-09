// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class CaretColor : ClassDictionaryBase
{
    public CaretColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "caret-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               caret-color: {0};
                               """,
                }
            },
            {
                "caret-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               caret-color: inherit;
                               """,
                }
            },
            {
                "caret-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               caret-color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}