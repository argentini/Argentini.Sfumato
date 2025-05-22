// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Overflow : ClassDictionaryBase
{
    public Overflow()
    {
        Group = "overflow";
        Description = "Utilities for controlling how content is clipped and scrolled.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "overflow-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: auto;
                               """
                }
            },
            {
                "overflow-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: hidden;
                               """
                }
            },
            {
                "overflow-clip", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: clip;
                               """
                }
            },
            {
                "overflow-visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: visible;
                               """
                }
            },
            {
                "overflow-scroll", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: scroll;
                               """
                }
            },
            {
                "overflow-x-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-x: auto;
                               """
                }
            },
            {
                "overflow-y-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-y: auto;
                               """
                }
            },
            {
                "overflow-x-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-x: hidden;
                               """
                }
            },
            {
                "overflow-y-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-y: hidden;
                               """
                }
            },
            {
                "overflow-x-clip", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-x: clip;
                               """
                }
            },
            {
                "overflow-y-clip", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-y: clip;
                               """
                }
            },
            {
                "overflow-x-visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-x: visible;
                               """
                }
            },
            {
                "overflow-y-visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-y: visible;
                               """
                }
            },
            {
                "overflow-x-scroll", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-x: scroll;
                               """
                }
            },
            {
                "overflow-y-scroll", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow-y: scroll;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}