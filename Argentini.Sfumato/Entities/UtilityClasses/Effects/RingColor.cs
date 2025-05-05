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
                    Template = 
                        """
                        --sf-ring-color: {0};
                        """,
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
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
