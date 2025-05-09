// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Visibility : ClassDictionaryBase
{
    public Visibility()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: visible;
                               """
                }
            },
            {
                "invisible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: hidden;
                               """
                }
            },
            {
                "collapse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: collapse;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}