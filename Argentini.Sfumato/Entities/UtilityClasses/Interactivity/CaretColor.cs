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
                    UsesColor = true,
                    Template = """
                               caret-color: {0};
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}