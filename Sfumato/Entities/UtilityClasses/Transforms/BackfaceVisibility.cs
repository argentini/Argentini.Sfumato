// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Transforms;

public sealed class BackfaceVisibility : ClassDictionaryBase
{
    public BackfaceVisibility()
    {
        Group = "backface-visibility";
        Description = "Utilities for controlling the visibility of the back face of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "backface-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        backface-visibility: hidden;
                        """,
                }
            },
            {
                "backface-visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        backface-visibility: visible;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}