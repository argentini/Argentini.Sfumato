// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class PlaceSelf : ClassDictionaryBase
{
    public PlaceSelf()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "place-self-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: auto;
                        """,
                }
            },
            {
                "place-self-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: start;
                        """,
                }
            },
            {
                "place-self-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: end;
                        """,
                }
            },
            {
                "place-self-end-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: safe end;
                        """,
                }
            },
            {
                "place-self-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: center;
                        """,
                }
            },
            {
                "place-self-center-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: safe center;
                        """,
                }
            },
            {
                "place-self-stretch", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        place-self: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}