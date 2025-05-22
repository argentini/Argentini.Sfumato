// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BoxSizing : ClassDictionaryBase
{
    public BoxSizing()
    {
        Group = "box-sizing";
        Description = "Utilities for setting box-sizing model.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "box-border", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               box-sizing: border-box;
                               """
                }
            },
            {
                "box-content", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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