// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BoxShadowColor : ClassDictionaryBase
{
    public BoxShadowColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesColor = true,
                    Template = 
                        """
                        --sf-shadow-color: {0};
                        """,
                }
            },
            {
                "shadow-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-shadow-color: inherit;
                        """,
                }
            },
            {
                "shadow-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-shadow-color: currentColor;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
