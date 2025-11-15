// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class PlaceContent : ClassDictionaryBase
{
    public PlaceContent()
    {
        Group = "place-content";
        Description = "Utilities for aligning content within grid or flex containers.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "place-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: center;
                        """,
                }
            },
            {
                "place-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: safe center;
                        """,
                }
            },
            {
                "place-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: start;
                        """,
                }
            },
            {
                "place-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: end;
                        """,
                }
            },
            {
                "place-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: safe end;
                        """,
                }
            },
            {
                "place-between", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: space-between;
                        """,
                }
            },
            {
                "place-around", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: space-around;
                        """,
                }
            },
            {
                "place-evenly", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: space-evenly;
                        """,
                }
            },
            {
                "place-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        place-content: stretch;
                        """,
                }
            },
            {
                "place-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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