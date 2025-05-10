// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextUnderlineOffset : ClassDictionaryBase
{
    public TextUnderlineOffset()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "underline-offset-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        text-underline-offset: {0}px;
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        text-underline-offset: {0};
                        """,
                }
            },
            {
                "-underline-offset-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        text-underline-offset: calc({0}px * -1);
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        text-underline-offset: calc({0} * -1);
                        """,
                }
            },
            {
                "underline-offset-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        text-underline-offset: auto;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}