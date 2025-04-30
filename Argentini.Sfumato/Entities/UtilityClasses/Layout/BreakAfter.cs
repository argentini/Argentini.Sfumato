// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BreakAfter : ClassDictionaryBase
{
    public BreakAfter()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-after-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: auto;
                               """
                }
            },
            {
                "break-after-avoid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: avoid;
                               """
                }
            },
            {
                "break-after-all", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: all;
                               """
                }
            },
            {
                "break-after-avoid-page", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: avoid-page;
                               """
                }
            },
            {
                "break-after-page", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: page;
                               """
                }
            },
            {
                "break-after-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: left;
                               """
                }
            },
            {
                "break-after-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: right;
                               """
                }
            },
            {
                "break-after-column", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               break-after: column;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}