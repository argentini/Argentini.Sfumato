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
                    UsesSlashModifier = true,
                    Template = 
                        """
                        --sf-shadow-color: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-shadow-color" ]
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
                    UsesCssCustomProperties = [ "--sf-shadow-color" ]
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
                    UsesCssCustomProperties = [ "--sf-shadow-color" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
