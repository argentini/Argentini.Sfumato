// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextWrap : ClassDictionaryBase
{
    public TextWrap()
    {
        Group = "text-wrap";
        Description = "Utilities for controlling how text wraps within an element.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-wrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-wrap: wrap;
                               """
                }
            },
            {
                "text-nowrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-wrap: nowrap;
                               """
                }
            },
            {
                "text-pretty", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-wrap: pretty;
                               """
                }
            },
            {
                "text-balance", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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