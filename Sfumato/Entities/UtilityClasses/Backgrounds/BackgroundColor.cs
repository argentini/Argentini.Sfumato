// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundColor : ClassDictionaryBase
{
    public BackgroundColor()
    {
        Group = "background-color";
        Description = "Utilities for setting background color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template = """
                               background-color: {0};
                               """,
                }
            },
            {
                "bg-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-color: inherit;
                               """,
                }
            },
            {
                "bg-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-color: currentColor;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}