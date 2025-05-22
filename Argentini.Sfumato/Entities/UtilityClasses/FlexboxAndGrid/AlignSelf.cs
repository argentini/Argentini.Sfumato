// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class AlignSelf : ClassDictionaryBase
{
    public AlignSelf()
    {
        Description = "Utilities for aligning a single item along the cross axis.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "self-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: auto;
                        """,
                }
            },
            {
                "self-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: flex-start;
                        """,
                }
            },
            {
                "self-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: flex-end;
                        """,
                }
            },
            {
                "self-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: safe flex-end;
                        """,
                }
            },
            {
                "self-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: center;
                        """,
                }
            },
            {
                "self-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: safe center;
                        """,
                }
            },
            {
                "self-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: baseline;
                        """,
                }
            },
            {
                "self-baseline-last", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: last baseline;
                        """,
                }
            },
            {
                "self-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-self: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}