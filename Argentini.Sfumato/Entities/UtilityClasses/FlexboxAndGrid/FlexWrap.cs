// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexWrap : ClassDictionaryBase
{
    public FlexWrap()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "flex-nowrap", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-wrap: nowrap;
                        """,
                }
            },
            {
                "flex-wrap", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-wrap: wrap;
                        """,
                }
            },
            {
                "flex-wrap-reverse", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        flex-wrap: wrap-reverse;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}