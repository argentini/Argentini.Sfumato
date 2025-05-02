// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class RingColor : ClassDictionaryBase
{
    public RingColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "ring-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = 
                        """
                        --sf-ring-color: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-ring-color" ]
                }
            },
            {
                "ring-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-ring-color: inherit;
                        """,
                    UsesCssCustomProperties = [ "--sf-ring-color" ]
                }
            },
            {
                "ring-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-ring-color: currentColor;
                        """,
                    UsesCssCustomProperties = [ "--sf-ring-color" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
