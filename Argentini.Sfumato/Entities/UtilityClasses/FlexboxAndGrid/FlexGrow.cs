// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexGrow : ClassDictionaryBase
{
    public FlexGrow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grow-", new ClassDefinition
                {
                    UsesInteger = true,
                    UsesAlphaNumber = true,
                    UsesDimensionLength = true,
                    UsesAbstractValue = true,
                    Template =
                        """
                        flex-grow: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        flex-grow: {0};
                        """,
                }
            },
            {
                "grow", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-grow: 1;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}