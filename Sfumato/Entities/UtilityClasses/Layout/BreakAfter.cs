// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class BreakAfter : ClassDictionaryBase
{
    public BreakAfter()
    {
        Group = "break-after";
        Description = "Utilities for configuring break after.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-after-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: auto;
                               """
                }
            },
            {
                "break-after-avoid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: avoid;
                               """
                }
            },
            {
                "break-after-all", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: all;
                               """
                }
            },
            {
                "break-after-avoid-page", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: avoid-page;
                               """
                }
            },
            {
                "break-after-page", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: page;
                               """
                }
            },
            {
                "break-after-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: left;
                               """
                }
            },
            {
                "break-after-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-after: right;
                               """
                }
            },
            {
                "break-after-column", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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