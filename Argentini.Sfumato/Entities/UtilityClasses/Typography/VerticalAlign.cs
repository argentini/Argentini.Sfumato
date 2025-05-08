// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class VerticalAlign : ClassDictionaryBase
{
    public VerticalAlign()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "align-", new ClassDefinition
                {
                    UsesDimensionLength = true,
                    Template =
                        """
                        vertical-align: {0};
                        """,
                }
            },
            {
                "align-baseline", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: baseline;
                        """,
                }
            },
            {
                "align-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: top;
                        """,
                }
            },
            {
                "align-middle", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: middle;
                        """,
                }
            },
            {
                "align-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: bottom;
                        """,
                }
            },
            {
                "align-text-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: text-top;
                        """,
                }
            },
            {
                "align-text-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: text-bottom;
                        """,
                }
            },
            {
                "align-sub", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        vertical-align: sub;
                        """,
                }
            },
            {
                "align-super", new ClassDefinition
                {
                    IsSimpleUtility = true,
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