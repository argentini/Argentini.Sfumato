// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class VerticalAlign : ClassDictionaryBase
{
    public VerticalAlign()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "align-", new ClassDefinition
                {
                    InLengthCollection = true,
                    Template =
                        """
                        vertical-align: {0};
                        """,
                }
            },
            {
                "align-baseline", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: baseline;
                        """,
                }
            },
            {
                "align-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: top;
                        """,
                }
            },
            {
                "align-middle", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: middle;
                        """,
                }
            },
            {
                "align-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: bottom;
                        """,
                }
            },
            {
                "align-text-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: text-top;
                        """,
                }
            },
            {
                "align-text-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: text-bottom;
                        """,
                }
            },
            {
                "align-sub", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: sub;
                        """,
                }
            },
            {
                "align-super", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        vertical-align: super;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}