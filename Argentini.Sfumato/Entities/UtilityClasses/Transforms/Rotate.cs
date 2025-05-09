// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Rotate : ClassDictionaryBase
{
    public Rotate()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "rotate-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        rotate: {0}deg;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        rotate: {0};
                        """,
                }
            },
            {
                "-rotate-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        rotate: calc({0}deg * -1);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        rotate: calc({0} * -1);
                        """,
                }
            },
            {
                "rotate-x-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-x: {0}deg;
                        transform: rotateX({0}deg) var(--sf-rotate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-x: {0};
                        transform: rotateX({0}) var(--sf-rotate-y);
                        """,
                }
            },
            {
                "-rotate-x-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-x: calc({0}deg * -1);
                        transform: rotateX(calc({0}deg * -1)) var(--sf-rotate-y);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-x: calc({0} * -1);
                        transform: rotateX(calc({0} * -1)) var(--sf-rotate-y);
                        """,
                }
            },
            {
                "rotate-y-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-y: {0}deg;
                        transform: var(--sf-rotate-x) rotateY({0}deg);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-y: {0};
                        transform: var(--sf-rotate-x) rotateY({0});
                        """,
                }
            },
            {
                "-rotate-y-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-y: calc({0}deg * -1);
                        transform: var(--sf-rotate-x) rotateY(calc({0}deg * -1));
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-y: calc({0} * -1);
                        transform: var(--sf-rotate-x) rotateY(calc({0} * -1));
                        """,
                }
            },
            {
                "rotate-z-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-z: {0}deg;
                        transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ({0}deg);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-z: {0};
                        transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ({0});
                        """,
                }
            },
            {
                "-rotate-z-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-rotate-z: calc({0}deg * -1);
                        transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ(calc({0}deg * -1));
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-rotate-z: calc({0} * -1);
                        transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ(calc({0} * -1));
                        """,
                }
            },
            {
                "rotate-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        rotate: none;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}