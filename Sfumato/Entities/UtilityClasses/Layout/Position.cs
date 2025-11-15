// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Position : ClassDictionaryBase
{
    public Position()
    {
        Group = "position";
        Description = "Utilities for controlling the positioning of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "static", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: static;
                               """
                }
            },
            {
                "fixed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: fixed;
                               """
                }
            },
            {
                "absolute", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: absolute;
                               """
                }
            },
            {
                "relative", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: relative;
                               """
                }
            },
            {
                "sticky", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               position: sticky;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}