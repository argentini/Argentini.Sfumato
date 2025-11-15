// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundOrigin : ClassDictionaryBase
{
    public BackgroundOrigin()
    {
        Group = "background-origin";
        Description = "Utilities for setting the origin position of background images.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-origin-border", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-origin: border-box;
                               """,
                }
            },
            {
                "bg-origin-padding", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-origin: padding-box;
                               """,
                }
            },
            {
                "bg-origin-content", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-origin: content-box;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}