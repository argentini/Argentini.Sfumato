// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class JustifySelf : ClassDictionaryBase
{
    public JustifySelf()
    {
        Group = "justify-self";
        Description = "Utilities for aligning a single item along the main axis.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "justify-self-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: auto;
                        """,
                }
            },
            {
                "justify-self-start", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: start;
                        """,
                }
            },
            {
                "justify-self-center", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: center;
                        """,
                }
            },
            {
                "justify-self-center-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: safe center;
                        """,
                }
            },
            {
                "justify-self-end", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: end;
                        """,
                }
            },
            {
                "justify-self-end-safe", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        justify-self: safe end;
                        """,
                }
            },
            {
                "justify-self-stretch", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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