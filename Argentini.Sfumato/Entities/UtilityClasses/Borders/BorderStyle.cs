// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BorderStyle : ClassDictionaryBase
{
    public BorderStyle()
    {
        Group = "border-style";
        Description = "Utilities for setting border style.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "border-solid", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: solid;
                        border-style: solid;
                        """,
                }
            },
            {
                "border-dashed", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: dashed;
                        border-style: dashed;
                        """,
                }
            },
            {
                "border-dotted", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: dotted;
                        border-style: dotted;
                        """,
                }
            },
            {
                "border-double", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: double;
                        border-style: double;
                        """,
                }
            },
            {
                "border-hidden", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: hidden;
                        border-style: hidden;
                        """,
                }
            },
            {
                "border-none", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-border-style: none;
                        border-style: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
