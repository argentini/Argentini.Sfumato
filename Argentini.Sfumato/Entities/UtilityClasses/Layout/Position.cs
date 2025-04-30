// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Position : ClassDictionaryBase
{
    public Position()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "static", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               position: static;
                               """
                }
            },
            {
                "fixed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               position: fixed;
                               """
                }
            },
            {
                "absolute", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               position: absolute;
                               """
                }
            },
            {
                "relative", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               position: relative;
                               """
                }
            },
            {
                "sticky", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               position: sticky;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}