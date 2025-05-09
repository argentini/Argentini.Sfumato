// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Scale : ClassDictionaryBase
{
    public Scale()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "scale-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesPercentage = true,
                    Template =
                        """
                        scale: {0}% {0}%;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        scale: {0} {0};
                        """,
                }
            },
            {
                "-scale-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    Template =
                        """
                        scale: calc({0}% * -1) calc({0}% * -1);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        scale: calc({0} * -1) calc({0} * -1);
                        """,
                }
            },
            {
                "scale-x-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-x: {0}%;
                        scale: {0}% var(--sf-scale-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-x: {0};
                        scale: {0} var(--sf-scale-y);
                        """,
                }
            },
            {
                "-scale-x-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-x: calc({0}% * -1);
                        scale: calc({0}% * -1) var(--sf-scale-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-x: calc({0} * -1);
                        scale: calc({0} * -1) var(--sf-scale-y);
                        """,
                }
            },
            {
                "scale-y-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-y: {0}%;
                        scale: var(--sf-scale-x) {0}%;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-y: {0};
                        scale: var(--sf-scale-x) {0};
                        """,
                }
            },
            {
                "-scale-y-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-y: calc({0}% * -1);
                        scale: var(--sf-scale-x) calc({0}% * -1);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-y: calc({0} * -1);
                        scale: var(--sf-scale-x) calc({0} * -1);
                        """,
                }
            },
            {
                "scale-z-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-z: {0}%;
                        scale: var(--sf-scale-x) var(--sf-scale-y) {0}%;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-z: {0};
                        scale: var(--sf-scale-x) var(--sf-scale-y) {0};
                        """,
                }
            },
            {
                "-scale-z-", new ClassDefinition
                {
                    UsesNumericSuffix = true,
                    UsesAngleHue = true,
                    Template =
                        """
                        --sf-scale-z: calc({0}% * -1);
                        scale: var(--sf-scale-x) var(--sf-scale-y) calc({0}% * -1);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-scale-z: calc({0} * -1);
                        scale: var(--sf-scale-x) var(--sf-scale-y) calc({0} * -1);
                        """,
                }
            },
            {
                "scale-3d", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        scale: var(--sf-scale-x) var(--sf-scale-y) var(--sf-scale-z);
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}