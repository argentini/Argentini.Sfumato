// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class OutlineStyle : ClassDictionaryBase
{
    public OutlineStyle()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "outline-solid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: solid;
                        """,
                }
            },
            {
                "outline-dashed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: dashed;
                        """,
                }
            },
            {
                "outline-dotted", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: dotted;
                        """,
                }
            },
            {
                "outline-double", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: double;
                        """,
                }
            },
            {
                "outline-hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: hidden;
                        """,
                }
            },
            {
                "outline-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        outline-style: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
