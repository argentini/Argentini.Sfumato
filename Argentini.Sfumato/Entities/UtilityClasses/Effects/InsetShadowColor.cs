// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetShadowColor : ClassDictionaryBase
{
    public InsetShadowColor()
    {
        Group = "box-shadow";
        Description = "Utilities for customizing the color of inner shadows.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InColorCollection = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: {0};
                        """,
                }
            },
            {
                "inset-shadow-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: inherit;
                        """,
                }
            },
            {
                "inset-shadow-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    InSimpleUtilityCollection = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: currentColor;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
