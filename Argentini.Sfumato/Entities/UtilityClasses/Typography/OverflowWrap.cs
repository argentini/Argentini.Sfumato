// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class OverflowWrap : ClassDictionaryBase
{
    public OverflowWrap()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "wrap-break-word", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        overflow-wrap: break-word;
                        """,
                }
            },
            {
                "wrap-anywhere", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        overflow-wrap: anywhere;
                        """,
                }
            },
            {
                "wrap-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        overflow-wrap: normal;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}