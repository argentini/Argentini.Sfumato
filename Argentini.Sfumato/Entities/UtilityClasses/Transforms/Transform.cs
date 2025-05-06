// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Transform : ClassDictionaryBase
{
    public Transform()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transform-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        transform: {0};
                        """,
                }
            },
            {
                "transform", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "transform-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform: none;
                        """,
                }
            },
            {
                "transform-cpu", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "transform-gpu", new ClassDefinition
                {
                    IsSimpleUtility = true,
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