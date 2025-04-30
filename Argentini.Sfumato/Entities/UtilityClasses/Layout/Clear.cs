// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Clear : ClassDictionaryBase
{
    public Clear()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "clear-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: left;
                               """
                }
            },
            {
                "clear-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: right;
                               """
                }
            },
            {
                "clear-both", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: both;
                               """
                }
            },
            {
                "clear-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: inline-start;
                               """
                }
            },
            {
                "clear-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: inline-end;
                               """
                }
            },
            {
                "clear-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               clear: none;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}