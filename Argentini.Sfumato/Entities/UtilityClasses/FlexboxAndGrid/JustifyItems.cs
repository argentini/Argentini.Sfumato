// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class JustifyItems : ClassDictionaryBase
{
    public JustifyItems()
    {
        Description = "Utilities for aligning items along the main axis.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "justify-items-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: start;
                        """,
                }
            },
            {
                "justify-items-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: end;
                        """,
                }
            },
            {
                "justify-items-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: safe end;
                        """,
                }
            },
            {
                "justify-items-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: center;
                        """,
                }
            },
            {
                "justify-items-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: safe center;
                        """,
                }
            },
            {
                "justify-items-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: stretch;
                        """,
                }
            },
            {
                "justify-items-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-items: normal;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}