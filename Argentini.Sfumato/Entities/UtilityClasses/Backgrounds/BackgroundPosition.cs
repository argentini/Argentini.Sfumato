// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundPosition : ClassDictionaryBase
{
    public BackgroundPosition()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-position-", new ClassDefinition
                {
                    UsesAbstractValue = true,
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
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: top left;
                        """,
                }
            },
            {
                "bg-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: top;
                        """,
                }
            },
            {
                "bg-top-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: top right;
                        """,
                }
            },
            {
                "bg-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: left;
                        """,
                }
            },
            {
                "bg-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: center;
                        """,
                }
            },
            {
                "bg-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: right;
                        """,
                }
            },
            {
                "bg-bottom-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: bottom left;
                        """,
                }
            },
            {
                "bg-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        background-position: bottom;
                        """,
                }
            },
            {
                "bg-bottom-right", new ClassDefinition
                {
                    IsSimpleUtility = true,
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