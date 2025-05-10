// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class JustifyContent : ClassDictionaryBase
{
    public JustifyContent()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "justify-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: flex-start;
                        """,
                }
            },
            {
                "justify-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: flex-end;
                        """,
                }
            },
            {
                "justify-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: safe flex-end;
                        """,
                }
            },
            {
                "justify-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: center;
                        """,
                }
            },
            {
                "justify-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: safe center;
                        """,
                }
            },
            {
                "justify-between", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: space-between;
                        """,
                }
            },
            {
                "justify-around", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: space-around;
                        """,
                }
            },
            {
                "justify-evenly", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: space-evenly;
                        """,
                }
            },
            {
                "justify-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: stretch;
                        """,
                }
            },
            {
                "justify-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: baseline;
                        """,
                }
            },
            {
                "justify-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-content: normal;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}