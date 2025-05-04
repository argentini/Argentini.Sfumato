// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TextShadowColor : ClassDictionaryBase
{
    public TextShadowColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = 
                        """
                        --sf-text-shadow-color: {0};
                        """,
                }
            },
            {
                "text-shadow-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-text-shadow-color: inherit;
                        """,
                }
            },
            {
                "text-shadow-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-text-shadow-color: currentColor;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
