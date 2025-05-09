// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Resize : ClassDictionaryBase
{
    public Resize()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "resize-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               resize: none;
                               """,
                }
            },
            {
                "resize", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               resize: both;
                               """,
                }
            },
            {
                "resize-y", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               resize: vertical;
                               """,
                }
            },
            {
                "resize-x", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               resize: horizontal;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}