// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetShadowColor : ClassDictionaryBase
{
    public InsetShadowColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-shadow-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-shadow-color" ]
                }
            },
            {
                "inset-shadow-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: inherit;
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-shadow-color" ]
                }
            },
            {
                "inset-shadow-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-inset-shadow-color: currentColor;
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-shadow-color" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
