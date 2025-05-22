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
                               --sf-content: {0};
                               content: var(--sf-content);
                               """
                }
            },
            {
                "content-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-content: none;
                               content: var(--sf-content);
                               """
                }
            },
       });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}