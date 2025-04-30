// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundClip : ClassDictionaryBase
{
    public BackgroundClip()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-clip-border", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-clip: border-box;
                               """,
                }
            },
            {
                "bg-clip-padding", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-clip: padding-box;
                               """,
                }
            },
            {
                "bg-clip-content", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-clip: content-box;
                               """,
                }
            },
            {
                "bg-clip-text", new ClassDefinition
                {
                    IsSimpleUtility = true,
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