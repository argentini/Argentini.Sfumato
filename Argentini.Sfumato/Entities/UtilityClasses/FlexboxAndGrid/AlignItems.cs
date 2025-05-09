// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class AlignItems : ClassDictionaryBase
{
    public AlignItems()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "items-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: flex-start;
                        """,
                }
            },
            {
                "items-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: flex-end;
                        """,
                }
            },
            {
                "items-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: safe flex-end;
                        """,
                }
            },
            {
                "items-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: center;
                        """,
                }
            },
            {
                "items-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: safe center;
                        """,
                }
            },
            {
                "items-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: baseline;
                        """,
                }
            },
            {
                "items-baseline-last", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: last baseline;
                        """,
                }
            },
            {
                "items-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-items: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}