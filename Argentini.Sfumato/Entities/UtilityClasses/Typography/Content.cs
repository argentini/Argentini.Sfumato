// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Content : ClassDictionaryBase
{
    public Content()
    {
        Group = "content";
        Description = "Utilities for setting the generated content of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "content-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InStringCollection = true,
                    Template = """
                               content: {0};
                               """
                }
            },
            {
                "content-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               content: none;
                               """
                }
            },
       });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}