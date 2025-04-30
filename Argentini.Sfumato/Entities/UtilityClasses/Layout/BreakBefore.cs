// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BreakBefore : ClassDictionaryBase
{
    public BreakBefore()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-before-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: auto;
                               """
                }
            },
            {
                "break-before-avoid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: avoid;
                               """
                }
            },
            {
                "break-before-all", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: all;
                               """
                }
            },
            {
                "break-before-avoid-page", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: avoid-page;
                               """
                }
            },
            {
                "break-before-page", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: page;
                               """
                }
            },
            {
                "break-before-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: left;
                               """
                }
            },
            {
                "break-before-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: right;
                               """
                }
            },
            {
                "break-before-column", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-before: column;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}