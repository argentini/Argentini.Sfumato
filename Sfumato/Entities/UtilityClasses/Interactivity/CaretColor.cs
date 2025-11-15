// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class CaretColor : ClassDictionaryBase
{
    public CaretColor()
    {
        Group = "caret-color";
        Description = "Utilities for customizing the caret color in inputs and textareas.";
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
                    InSimpleUtilityCollection = true,
                    Template = """
                               caret-color: inherit;
                               """,
                }
            },
            {
                "caret-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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