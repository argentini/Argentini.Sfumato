// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class UserSelect : ClassDictionaryBase
{
    public UserSelect()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "select-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               user-select: none;
                               """,
                }
            },
            {
                "select-text", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               user-select: text;
                               """,
                }
            },
            {
                "select-all", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               user-select: all;
                               """,
                }
            },
            {
                "select-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               user-select: auto;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}