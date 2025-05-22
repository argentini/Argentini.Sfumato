// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineStyle : ClassDictionaryBase
{
    public OutlineStyle()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-solid", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: solid;
                        outline-style: solid;
                        """,
                }
            },
            {
                "outline-dashed", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: dashed;
                        outline-style: dashed;
                        """,
                }
            },
            {
                "outline-dotted", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: dotted;
                        outline-style: dotted;
                        """,
                }
            },
            {
                "outline-double", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: double;
                        outline-style: double;
                        """,
                }
            },
            {
                "outline-hidden", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: hidden;
                        outline-style: hidden;
                        """,
                }
            },
            {
                "outline-none", new ClassDefinition
                {
                    SelectorSort = 2,
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        --sf-outline-style: none;
                        outline-style: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
