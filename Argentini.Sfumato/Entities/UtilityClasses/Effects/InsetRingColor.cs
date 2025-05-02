// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class InsetRingColor : ClassDictionaryBase
{
    public InsetRingColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "inset-ring-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = 
                        """
                        --sf-inset-ring-color: {0};
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-ring-color" ]
                }
            },
            {
                "inset-ring-inherit", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-inset-ring-color: inherit;
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-ring-color" ]
                }
            },
            {
                "inset-ring-current", new ClassDefinition
                {
                    SelectorSort = 1,
                    IsSimpleUtility = true,
                    Template = 
                        """
                        --sf-inset-ring-color: currentColor;
                        """,
                    UsesCssCustomProperties = [ "--sf-inset-ring-color" ]
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
