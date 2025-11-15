// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextAlign : ClassDictionaryBase
{
    public TextAlign()
    {
        Group = "text-align";
        Description = "Utilities for setting text alignment.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-align: left;
                               """
                }
            },
            {
                "text-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-align: center;
                               """
                }
            },
            {
                "text-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-align: right;
                               """
                }
            },
            {
                "text-justify", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-align: justify;
                               """
                }
            },
            {
                "text-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-align: start;
                               """
                }
            },
            {
                "text-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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