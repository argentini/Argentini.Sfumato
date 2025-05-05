// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideColor : ClassDictionaryBase
{
    public DivideColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-", new ClassDefinition
                {
                    UsesColor = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-color: {0};
                        }
                        """,
                }
            },
            {
                "divide-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-color: inherit;
                        }
                        """,
                }
            },
            {
                "divide-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        & > :not(:last-child) {
                            border-color: currentColor;
                        }
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
