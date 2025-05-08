// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Content : ClassDictionaryBase
{
    public Content()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "content-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesString = true,
                    Template = """
                               content: {0};
                               """
                }
            },
            {
                "content-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
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