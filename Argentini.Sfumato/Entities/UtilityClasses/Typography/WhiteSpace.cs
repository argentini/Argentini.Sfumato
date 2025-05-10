// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class WhiteSpace : ClassDictionaryBase
{
    public WhiteSpace()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "whitespace-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: normal;
                        """,
                }
            },
            {
                "whitespace-nowrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: nowrap;
                        """,
                }
            },
            {
                "whitespace-pre", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: pre;
                        """,
                }
            },
            {
                "whitespace-pre-line", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: pre-line;
                        """,
                }
            },
            {
                "whitespace-pre-wrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: pre-wrap;
                        """,
                }
            },
            {
                "whitespace-break-spaces", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        white-space: break-spaces;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}