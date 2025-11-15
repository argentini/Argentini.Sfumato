// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class FromColor : ClassDictionaryBase
{
    public FromColor()
    {
        Group = "background-image";
        Description = "Utilities for configuring background gradient from color.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "from-", new ClassDefinition
                {
                    InColorCollection = true,
                    SelectorSort = 1,
                    Template =
                        """
                        --sf-gradient-from: {0};
                        --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}