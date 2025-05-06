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
                    IsSimpleUtility = true,
                    Template = """
                               resize: none;
                               """,
                }
            },
            {
                "resize", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               resize: both;
                               """,
                }
            },
            {
                "resize-y", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               resize: vertical;
                               """,
                }
            },
            {
                "resize-x", new ClassDefinition
                {
                    IsSimpleUtility = true,
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