// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class Skew : ClassDictionaryBase
{
    public Skew()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "skew-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-x: skewX({0}deg);
                        --sf-skew-y: skewY({0}deg);
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-x: skewX({0});
                        --sf-skew-y: skewY({0});
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "-skew-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-x: skewX(calc({0}deg * -1));
                        --sf-skew-y: skewY(calc({0}deg * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-x: skewX(calc({0} * -1));
                        --sf-skew-y: skewY(calc({0} * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "skew-x-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-x: skewX({0}deg);
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-x: skewX({0});
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "-skew-x-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-x: skewX(calc({0}deg * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-x: skewX(calc({0} * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "skew-y-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-y: skewY({0}deg);
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-y: skewY({0});
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
            {
                "-skew-y-", new ClassDefinition
                {
                    InAngleHueCollection = true,
                    Template =
                        """
                        --sf-skew-y: skewY(calc({0}deg * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-skew-y: skewY(calc({0} * -1));
                        transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}