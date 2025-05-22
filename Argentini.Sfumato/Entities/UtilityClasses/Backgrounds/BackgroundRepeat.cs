// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundRepeat : ClassDictionaryBase
{
    public BackgroundRepeat()
    {
        Description = "Utilities for defining how background images repeat.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-repeat", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: repeat;
                               """,
                }
            },
            {
                "bg-repeat-x", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: repeat-x;
                               """,
                }
            },
            {
                "bg-repeat-y", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: repeat-y;
                               """,
                }
            },
            {
                "bg-repeat-space", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: space;
                               """,
                }
            },
            {
                "bg-repeat-round", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: round;
                               """,
                }
            },
            {
                "bg-no-repeat", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               background-repeat: no-repeat;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}