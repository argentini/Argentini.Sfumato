// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Isolation : ClassDictionaryBase
{
    public Isolation()
    {
        Group = "isolation";
        Description = "Utilities for controlling isolation and stacking context.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "isolate", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               isolation: isolate;
                               """
                }
            },
            {
                "isolate-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               isolation: auto;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}