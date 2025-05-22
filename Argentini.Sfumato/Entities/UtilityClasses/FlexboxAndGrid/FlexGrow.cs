// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexGrow : ClassDictionaryBase
{
    public FlexGrow()
    {
        Group = "flex-grow";
        Description = "Utilities for controlling how flex items grow to fill space.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "grow-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    InFlexCollection = true,
                    InAbstractValueCollection = true,
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
                    InSimpleUtilityCollection = true,
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