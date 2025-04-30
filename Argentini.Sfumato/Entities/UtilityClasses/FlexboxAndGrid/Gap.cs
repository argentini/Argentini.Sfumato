// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class Gap : ClassDictionaryBase
{
    public Gap()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "gap-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        gap: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "gap-x-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               column-gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        column-gap: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
            {
                "gap-y-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesDimensionLength = true,
                    Template = """
                               row-gap: calc(var(--spacing) * {0});
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        row-gap: {0};
                        """,
                    UsesCssCustomProperties = [ "--spacing" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
