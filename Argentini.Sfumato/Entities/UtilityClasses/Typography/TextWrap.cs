// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextWrap : ClassDictionaryBase
{
    public TextWrap()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-pretty", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-wrap: pretty;
                               """
                }
            },
            {
                "text-balance", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-wrap: balance;
                               """
                }
            },
       });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}