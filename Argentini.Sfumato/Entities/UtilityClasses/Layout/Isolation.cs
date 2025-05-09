// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Isolation : ClassDictionaryBase
{
    public Isolation()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "isolate", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               isolation: isolate;
                               """
                }
            },
            {
                "isolate-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               isolation: auto;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}