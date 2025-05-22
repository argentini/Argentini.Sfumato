// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class Gap : ClassDictionaryBase
{
    public Gap()
    {
        Group = "gap";
        Description = "Utilities for defining gaps between flex or grid items.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "gap-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        gap: {0};
                        """,
                }
            },
            {
                "gap-x-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               column-gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        column-gap: {0};
                        """,
                }
            },
            {
                "gap-y-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template = """
                               row-gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        row-gap: {0};
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
