// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class BreakBefore : ClassDictionaryBase
{
    public BreakBefore()
    {
        Group = "break before";
        Description = "Utilities for configuring break before.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-before-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: auto;
                               """
                }
            },
            {
                "break-before-avoid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: avoid;
                               """
                }
            },
            {
                "break-before-all", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: all;
                               """
                }
            },
            {
                "break-before-avoid-page", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: avoid-page;
                               """
                }
            },
            {
                "break-before-page", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: page;
                               """
                }
            },
            {
                "break-before-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: left;
                               """
                }
            },
            {
                "break-before-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-before: right;
                               """
                }
            },
            {
                "break-before-column", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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