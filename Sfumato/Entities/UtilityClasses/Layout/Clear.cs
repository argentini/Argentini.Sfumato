// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Clear : ClassDictionaryBase
{
    public Clear()
    {
        Group = "clear";
        Description = "Utilities for controlling the clearing of floated elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "clear-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               clear: left;
                               """
                }
            },
            {
                "clear-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               clear: right;
                               """
                }
            },
            {
                "clear-both", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               clear: both;
                               """
                }
            },
            {
                "clear-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               clear: inline-start;
                               """
                }
            },
            {
                "clear-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               clear: inline-end;
                               """
                }
            },
            {
                "clear-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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