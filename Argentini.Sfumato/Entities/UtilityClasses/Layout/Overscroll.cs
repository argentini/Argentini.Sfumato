// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Overscroll : ClassDictionaryBase
{
    public Overscroll()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "overscroll-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior: auto;
                               """
                }
            },
            {
                "overscroll-contain", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior: contain;
                               """
                }
            },
            {
                "overscroll-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior: none;
                               """
                }
            },
            {
                "overscroll-x-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-x: auto;
                               """
                }
            },
            {
                "overscroll-x-contain", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-x: contain;
                               """
                }
            },
            {
                "overscroll-x-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-x: none;
                               """
                }
            },
            {
                "overscroll-y-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-y: auto;
                               """
                }
            },
            {
                "overscroll-y-contain", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-y: contain;
                               """
                }
            },
            {
                "overscroll-y-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               overscroll-behavior-y: none;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}