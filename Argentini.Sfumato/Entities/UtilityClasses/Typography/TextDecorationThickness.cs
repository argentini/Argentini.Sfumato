// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationThickness : ClassDictionaryBase
{
    public TextDecorationThickness()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "decoration-", new ClassDefinition
                {
                    UsesSpacing = true,
                    UsesAlphaNumber = true,
                    UsesDimensionLength = true,
                    Template =
                        """
                        text-decoration-thickness: {0}px;
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        text-decoration-thickness: {0};
                        """,
                }
            },
            {
                "decoration-from-font", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        text-decoration-thickness: from-font;
                        """,
                }
            },
            {
                "decoration-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        text-decoration-thickness: auto;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}