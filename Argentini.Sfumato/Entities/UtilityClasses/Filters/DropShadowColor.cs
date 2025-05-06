// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Filters;

public sealed class DropShadowColor : ClassDictionaryBase
{
    public DropShadowColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "drop-shadow-", new ClassDefinition
                {
                    UsesColor = true,
                    Template =
                        """
                        --sf-drop-shadow-color: {0};
                        --sf-drop-shadow: var(--sf-drop-shadow-size)
                        """,
                }
            },
            {
                "drop-shadow-inherit", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               --sf-drop-shadow-color: inherit;
                               --sf-drop-shadow: var(--sf-drop-shadow-size)
                               """,
                }
            },
            {
                "drop-shadow-current", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               --sf-drop-shadow-color: currentColor;
                               --sf-drop-shadow: var(--sf-drop-shadow-size)
                               """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}