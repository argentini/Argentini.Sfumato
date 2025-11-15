// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundAttachment : ClassDictionaryBase
{
    public BackgroundAttachment()
    {
        Group = "background-attachment";
        Description = "Utilities for controlling background attachment to the viewport.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-fixed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-attachment: fixed;
                               """,
                }
            },
            {
                "bg-local", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-attachment: local;
                               """,
                }
            },
            {
                "bg-scroll", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-attachment: scroll;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}