// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class AlignContent : ClassDictionaryBase
{
    public AlignContent()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "content-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: normal;
                        """,
                }
            },
            {
                "content-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: center;
                        """,
                }
            },
            {
                "content-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: flex-start;
                        """,
                }
            },
            {
                "content-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: flex-end;
                        """,
                }
            },
            {
                "content-between", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: space-between;
                        """,
                }
            },
            {
                "content-around", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: space-around;
                        """,
                }
            },
            {
                "content-evenly", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: space-evenly;
                        """,
                }
            },
            {
                "content-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: baseline;
                        """,
                }
            },
            {
                "content-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        align-content: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}