// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Filters;

public sealed class DropShadowColor : ClassDictionaryBase
{
    public DropShadowColor()
    {
        Group = "filter/drop-shadow";
        Description = "Utilities for configuring drop shadow color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "drop-shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-drop-shadow-color: {0};
                        """,
                }
            },
            {
                "drop-shadow-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-drop-shadow-color: inherit;
                               """,
                }
            },
            {
                "drop-shadow-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-drop-shadow-color: currentColor;
                               """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}