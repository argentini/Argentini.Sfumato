// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class OverflowWrap : ClassDictionaryBase
{
    public OverflowWrap()
    {
        Group = "overflow-wrap";
        Description = "Utilities for handling overflow wrapping of text.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "wrap-break-word", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        overflow-wrap: break-word;
                        """,
                }
            },
            {
                "wrap-anywhere", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        overflow-wrap: anywhere;
                        """,
                }
            },
            {
                "wrap-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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