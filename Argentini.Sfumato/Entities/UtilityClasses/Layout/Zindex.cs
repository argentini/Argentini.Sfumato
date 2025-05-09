// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Zindex : ClassDictionaryBase
{
    public Zindex()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "z-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    Template = """
                               z-index: {0};
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        z-index: {0};
                        """,
                }
            },
            {
                "-z-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    Template = """
                               z-index: -{0};
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        z-index: -{0};
                        """,
                }
            },
            {
                "z-auto", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               z-index: auto;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
