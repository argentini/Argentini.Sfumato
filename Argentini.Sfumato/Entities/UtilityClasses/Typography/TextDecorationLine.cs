// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationLine : ClassDictionaryBase
{
    public TextDecorationLine()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "underline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-line: underline;
                               """
                }
            },
            {
                "overline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-line: overline;
                               """
                }
            },
            {
                "line-through", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-line: line-through;
                               """
                }
            },
            {
                "no-underline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-line: none;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}