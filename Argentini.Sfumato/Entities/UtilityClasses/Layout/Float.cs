// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Float : ClassDictionaryBase
{
    public Float()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "float-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               float: right;
                               """
                }
            },
            {
                "float-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               float: left;
                               """
                }
            },
            {
                "float-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               float: inline-start;
                               """
                }
            },
            {
                "float-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               float: inline-end;
                               """
                }
            },
            {
                "float-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               float: none;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}