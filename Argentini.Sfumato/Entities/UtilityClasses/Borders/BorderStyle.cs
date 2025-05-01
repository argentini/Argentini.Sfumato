// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BorderStyle : ClassDictionaryBase
{
    public BorderStyle()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "border-solid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: solid;
                        """,
                }
            },
            {
                "border-dashed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: dashed;
                        """,
                }
            },
            {
                "border-dotted", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: dotted;
                        """,
                }
            },
            {
                "border-double", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: double;
                        """,
                }
            },
            {
                "border-hidden", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: hidden;
                        """,
                }
            },
            {
                "border-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-style: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
