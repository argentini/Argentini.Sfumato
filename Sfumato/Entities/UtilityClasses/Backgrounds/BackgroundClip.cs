// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundClip : ClassDictionaryBase
{
    public BackgroundClip()
    {
        Group = "background-clip";
        Description = "Utilities for setting background clipping area.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-clip-border", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-clip: border-box;
                               """,
                }
            },
            {
                "bg-clip-padding", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-clip: padding-box;
                               """,
                }
            },
            {
                "bg-clip-content", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-clip: content-box;
                               """,
                }
            },
            {
                "bg-clip-text", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-clip: text;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}