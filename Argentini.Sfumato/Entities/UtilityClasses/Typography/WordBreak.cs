// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class WordBreak : ClassDictionaryBase
{
    public WordBreak()
    {
        Group = "word-break";
        Description = "Utilities for controlling how words break across lines.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "break-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        word-break: normal;
                        """,
                }
            },
            {
                "break-all", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        word-break: break-all;
                        """,
                }
            },
            {
                "break-keep", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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