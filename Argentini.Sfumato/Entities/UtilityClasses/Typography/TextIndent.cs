// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextIndent : ClassDictionaryBase
{
    public TextIndent()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "indent-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        text-indent: calc(var(--spacing) * {0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        text-indent: {0};
                        """,
                }
            },
            {
                "-indent-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    InLengthCollection = true,
                    Template =
                        """
                        text-indent: calc(var(--spacing) * -{0});
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        text-indent: calc({0} * -1);
                        """,
                }
            },
            {
                "indent-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        text-indent: 1px;
                        """,
                }
            },
            {
                "-indent-px", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        text-indent: -1px;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}