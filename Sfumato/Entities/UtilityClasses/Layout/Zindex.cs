// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Zindex : ClassDictionaryBase
{
    public Zindex()
    {
        Group = "z-index";
        Description = "Utilities for controlling the stack order of elements.";
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
                    InSimpleUtilityCollection = true,
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
