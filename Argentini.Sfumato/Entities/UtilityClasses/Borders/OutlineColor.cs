// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineColor : ClassDictionaryBase
{
    public OutlineColor()
    {
        Group = "outline-color";
        Description = "Utilities for setting outline color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-", new ClassDefinition
                {
                    InColorCollection = true,
                    Template =
                        """
                        outline-color: {0};
                        """,
                }
            },
            {
                "outline-inherit", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        outline-color: inherit;
                        """,
                }
            },
            {
                "outline-current", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        outline-color: currentColor;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
