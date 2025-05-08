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
                    IsSimpleUtility = true,
                    Template =
                        """
                        white-space: normal;
                        """,
                }
            },
            {
                "whitespace-nowrap", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        white-space: nowrap;
                        """,
                }
            },
            {
                "whitespace-pre", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        white-space: pre;
                        """,
                }
            },
            {
                "whitespace-pre-line", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        white-space: pre-line;
                        """,
                }
            },
            {
                "whitespace-pre-wrap", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        white-space: pre-wrap;
                        """,
                }
            },
            {
                "whitespace-break-spaces", new ClassDefinition
                {
                    IsSimpleUtility = true,
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