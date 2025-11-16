// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexDirection : ClassDictionaryBase
{
    public FlexDirection()
    {
        Group = "flex-direction";
        Description = "Utilities for setting the direction of flex container items.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "flex-row", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-direction: row;
                        """,
                }
            },
            {
                "flex-row-reverse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-direction: row-reverse;
                        """,
                }
            },
            {
                "flex-col", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-direction: column;
                        """,
                }
            },
            {
                "flex-col-reverse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-direction: column-reverse;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}