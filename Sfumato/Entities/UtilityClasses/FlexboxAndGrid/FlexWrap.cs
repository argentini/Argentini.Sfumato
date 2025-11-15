// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.FlexboxAndGrid;

public sealed class FlexWrap : ClassDictionaryBase
{
    public FlexWrap()
    {
        Group = "flex-wrap";
        Description = "Utilities for controlling flex wrapping behavior.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "flex-nowrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-wrap: nowrap;
                        """,
                }
            },
            {
                "flex-wrap", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-wrap: wrap;
                        """,
                }
            },
            {
                "flex-wrap-reverse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        flex-wrap: wrap-reverse;
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}