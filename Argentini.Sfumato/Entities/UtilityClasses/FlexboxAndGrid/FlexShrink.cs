// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexShrink : ClassDictionaryBase
{
    public FlexShrink()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shrink-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAlphaNumber = true,
                    UsesDimensionLength = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        flex-shrink: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-shrink: {0};
                        """,
                }
            },
            {
                "shrink", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-shrink: 1;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}