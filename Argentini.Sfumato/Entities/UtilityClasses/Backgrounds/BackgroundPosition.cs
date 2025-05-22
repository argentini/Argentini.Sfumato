// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundPosition : ClassDictionaryBase
{
    public BackgroundPosition()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-position-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        background-position: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        background-position: {0};
                        """, 
                }
            },
            {
                "bg-top-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: top left;
                        """,
                }
            },
            {
                "bg-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: top;
                        """,
                }
            },
            {
                "bg-top-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: top right;
                        """,
                }
            },
            {
                "bg-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: left;
                        """,
                }
            },
            {
                "bg-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: center;
                        """,
                }
            },
            {
                "bg-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: right;
                        """,
                }
            },
            {
                "bg-bottom-left", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: bottom left;
                        """,
                }
            },
            {
                "bg-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: bottom;
                        """,
                }
            },
            {
                "bg-bottom-right", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        background-position: bottom right;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}