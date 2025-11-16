// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class BoxDecorationBreak : ClassDictionaryBase
{
    public BoxDecorationBreak()
    {
        Group = "box-decoration-break";
        Description = "Utilities for controlling box decoration break for fragments.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "box-decoration-clone", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               box-decoration-break: clone;
                               """
                }
            },
            {
                "box-decoration-slice", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               box-decoration-break: slice;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}