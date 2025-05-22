// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BoxShadowColor : ClassDictionaryBase
{
    public BoxShadowColor()
    {
        Group = "box-shadow";
        Description = "Utilities for configuring box shadow color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InColorCollection = true,
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
                    InSimpleUtilityCollection = true,
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
                    InSimpleUtilityCollection = true,
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
