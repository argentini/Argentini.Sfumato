// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideStyle : ClassDictionaryBase
{
    public DivideStyle()
    {
        Group = "border-style";
        Description = "Utilities for setting divider style between elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-solid", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: solid;
                        }
                        """,
                }
            },
            {
                "divide-dashed", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: dashed;
                        }
                        """,
                }
            },
            {
                "divide-dotted", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: dotted;
                        }
                        """,
                }
            },
            {
                "divide-double", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: double;
                        }
                        """,
                }
            },
            {
                "divide-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: hidden;
                        }
                        """,
                }
            },
            {
                "divide-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-style: none;
                        }
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
