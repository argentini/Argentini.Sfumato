// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BoxSizing : ClassDictionaryBase
{
    public BoxSizing()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "box-border", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               box-sizing: border-box;
                               """
                }
            },
            {
                "box-content", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               box-sizing: content-box;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}