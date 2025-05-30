// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class BreakInside : ClassDictionaryBase
{
    public BreakInside()
    {
        Group = "break-inside";
        Description = "Utilities for configuring break inside.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-inside-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-inside: auto;
                               """
                }
            },
            {
                "break-inside-avoid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-inside: avoid;
                               """
                }
            },
            {
                "break-inside-avoid-page", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-inside: avoid-page;
                               """
                }
            },
            {
                "break-inside-avoid-column", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               break-inside: avoid-column;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}