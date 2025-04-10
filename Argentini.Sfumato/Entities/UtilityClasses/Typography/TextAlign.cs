// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextAlign : ClassDictionaryBase
{
    public TextAlign()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: left;
                               """
                }
            },
            {
                "text-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: center;
                               """
                }
            },
            {
                "text-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: right;
                               """
                }
            },
            {
                "text-justify", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: justify;
                               """
                }
            },
            {
                "text-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: start;
                               """
                }
            },
            {
                "text-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-align: end;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}