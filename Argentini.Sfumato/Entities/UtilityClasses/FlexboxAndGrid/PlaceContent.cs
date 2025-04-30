// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class PlaceContent : ClassDictionaryBase
{
    public PlaceContent()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "place-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: center;
                        """,
                }
            },
            {
                "place-center-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: safe center;
                        """,
                }
            },
            {
                "place-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: start;
                        """,
                }
            },
            {
                "place-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: end;
                        """,
                }
            },
            {
                "place-end-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: safe end;
                        """,
                }
            },
            {
                "place-between", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: space-between;
                        """,
                }
            },
            {
                "place-around", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: space-around;
                        """,
                }
            },
            {
                "place-evenly", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: space-evenly;
                        """,
                }
            },
            {
                "place-stretch", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: stretch;
                        """,
                }
            },
            {
                "place-baseline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-content: baseline;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}