// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Overflow : ClassDictionaryBase
{
    public Overflow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "overflow-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow: auto;
                               """
                }
            },
            {
                "overflow-hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow: hidden;
                               """
                }
            },
            {
                "overflow-clip", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow: clip;
                               """
                }
            },
            {
                "overflow-visible", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow: visible;
                               """
                }
            },
            {
                "overflow-scroll", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow: scroll;
                               """
                }
            },
            {
                "overflow-x-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-x: auto;
                               """
                }
            },
            {
                "overflow-y-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-y: auto;
                               """
                }
            },
            {
                "overflow-x-hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-x: hidden;
                               """
                }
            },
            {
                "overflow-y-hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-y: hidden;
                               """
                }
            },
            {
                "overflow-x-clip", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-x: clip;
                               """
                }
            },
            {
                "overflow-y-clip", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-y: clip;
                               """
                }
            },
            {
                "overflow-x-visible", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-x: visible;
                               """
                }
            },
            {
                "overflow-y-visible", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-y: visible;
                               """
                }
            },
            {
                "overflow-x-scroll", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overflow-x: scroll;
                               """
                }
            },
            {
                "overflow-y-scroll", new ClassDefinition
                {
                    IsSimpleUtility = true,
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