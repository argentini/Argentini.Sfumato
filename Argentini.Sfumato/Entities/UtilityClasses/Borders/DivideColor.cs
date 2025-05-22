// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DivideColor : ClassDictionaryBase
{
    public DivideColor()
    {
        Group = "border-color";
        Description = "Utilities for setting divider color between elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "divide-", new ClassDefinition
                {
                    InColorCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
