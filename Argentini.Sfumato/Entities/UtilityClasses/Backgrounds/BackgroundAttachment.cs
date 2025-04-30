// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundAttachment : ClassDictionaryBase
{
    public BackgroundAttachment()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-fixed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-attachment: fixed;
                               """,
                }
            },
            {
                "bg-local", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               background-attachment: local;
                               """,
                }
            },
            {
                "bg-scroll", new ClassDefinition
                {
                    IsSimpleUtility = true,
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