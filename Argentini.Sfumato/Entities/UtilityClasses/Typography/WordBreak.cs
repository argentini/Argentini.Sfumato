// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class WordBreak : ClassDictionaryBase
{
    public WordBreak()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        word-break: normal;
                        """,
                }
            },
            {
                "break-all", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        word-break: break-all;
                        """,
                }
            },
            {
                "break-keep", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        word-break: keep-all;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}