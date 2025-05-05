// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskComposite : ClassDictionaryBase
{
    public MaskComposite()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-add", new ClassDefinition
                {
                    SelectorSort = 4,
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-composite: add;
                        """,
                }
            },
            {
                "mask-subtract", new ClassDefinition
                {
                    SelectorSort = 4,
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-composite: subtract;
                        """,
                }
            },
            {
                "mask-intersect", new ClassDefinition
                {
                    SelectorSort = 4,
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-exclude", new ClassDefinition
                {
                    SelectorSort = 4,
                    IsSimpleUtility = true,
                    Template =
                        """
                        mask-composite: exclude;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
