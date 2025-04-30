// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class JustifySelf : ClassDictionaryBase
{
    public JustifySelf()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "justify-self-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: auto;
                        """,
                }
            },
            {
                "justify-self-start", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: start;
                        """,
                }
            },
            {
                "justify-self-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: center;
                        """,
                }
            },
            {
                "justify-self-center-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: safe center;
                        """,
                }
            },
            {
                "justify-self-end", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: end;
                        """,
                }
            },
            {
                "justify-self-end-safe", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: safe end;
                        """,
                }
            },
            {
                "justify-self-stretch", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        justify-self: stretch;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}