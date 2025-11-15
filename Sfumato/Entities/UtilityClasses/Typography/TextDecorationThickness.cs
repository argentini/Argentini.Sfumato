// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationThickness : ClassDictionaryBase
{
    public TextDecorationThickness()
    {
        Group = "text-decoration-thickness";
        Description = "Utilities for setting the thickness of text decorations.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "decoration-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
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
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        text-decoration-thickness: from-font;
                        """,
                }
            },
            {
                "decoration-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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