// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexDirection : ClassDictionaryBase
{
    public FlexDirection()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "flex-row", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-direction: row;
                        """,
                }
            },
            {
                "flex-row-reverse", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-direction: row-reverse;
                        """,
                }
            },
            {
                "flex-col", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-direction: column;
                        """,
                }
            },
            {
                "flex-col-reverse", new ClassDefinition
                {
                    IsSimpleUtility = true,
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