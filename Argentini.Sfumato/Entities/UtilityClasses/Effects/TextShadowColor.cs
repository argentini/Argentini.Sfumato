// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TextShadowColor : ClassDictionaryBase
{
    public TextShadowColor()
    {
        Group = "text-shadow";
        Description = "Utilities for configuring text shadow color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InColorCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
