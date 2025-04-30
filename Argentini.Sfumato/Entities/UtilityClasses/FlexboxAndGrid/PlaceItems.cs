// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class PlaceItems : ClassDictionaryBase
{
    public PlaceItems()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "place-items-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: start;
                        """,
                }
            },
            {
                "place-items-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: end;
                        """,
                }
            },
            {
                "place-items-end-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: safe end;
                        """,
                }
            },
            {
                "place-items-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: center;
                        """,
                }
            },
            {
                "place-items-center-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: safe center;
                        """,
                }
            },
            {
                "place-items-baseline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: baseline;
                        """,
                }
            },
            {
                "place-items-stretch", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-items: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}