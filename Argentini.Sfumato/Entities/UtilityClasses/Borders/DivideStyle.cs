// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideStyle : ClassDictionaryBase
{
    public DivideStyle()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-solid", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
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
