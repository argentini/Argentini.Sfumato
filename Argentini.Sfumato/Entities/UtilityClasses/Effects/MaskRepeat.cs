// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MaskRepeat : ClassDictionaryBase
{
    public MaskRepeat()
    {
        Group = "mask-repeat";
        Description = "Utilities for specifying repetition of masks.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-repeat", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: repeat;
                        mask-repeat: repeat;
                        """,
                }
            },
            {
                "mask-no-repeat", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: no-repeat;
                        mask-repeat: no-repeat;
                        """,
                }
            },
            {
                "mask-repeat-x", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: repeat-x;
                        mask-repeat: repeat-x;
                        """,
                }
            },
            {
                "mask-repeat-y", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: repeat-y;
                        mask-repeat: repeat-y;
                        """,
                }
            },
            {
                "mask-repeat-space", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: space;
                        mask-repeat: space;
                        """,
                }
            },
            {
                "mask-repeat-round", new ClassDefinition
                {
                    SelectorSort = 4,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        -webkit-mask-repeat: round;
                        mask-repeat: round;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
