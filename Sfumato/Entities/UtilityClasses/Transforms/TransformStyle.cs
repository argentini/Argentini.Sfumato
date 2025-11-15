// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Transforms;

public sealed class TransformStyle : ClassDictionaryBase
{
    public TransformStyle()
    {
        Group = "transform-style";
        Description = "Utilities for setting the style for nested transforms.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transform-3d", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform-style: preserve-3d;
                        """,
                }
            },
            {
                "transform-flat", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform-style: flat;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}