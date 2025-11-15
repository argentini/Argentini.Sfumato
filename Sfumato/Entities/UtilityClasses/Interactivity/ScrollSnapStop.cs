// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapStop : ClassDictionaryBase
{
    public ScrollSnapStop()
    {
        Group = "scroll-snap-stop";
        Description = "Utilities for setting scroll snap stop behavior.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "snap-normal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-stop: normal;
                               """,
                }
            },
            {
                "snap-always", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               scroll-snap-stop: always;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}