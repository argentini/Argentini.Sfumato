// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Transform : ClassDictionaryBase
{
    public Transform()
    {
        Group = "transform";
        Description = "Utilities for applying 2D and 3D transforms to elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transform-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template =
                        """
                        transform: {0};
                        """,
                }
            },
            {
                "transform", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "transform-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform: none;
                        """,
                }
            },
            {
                "transform-cpu", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "transform-gpu", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        transform: translateZ(0) var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}