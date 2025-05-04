// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class Mask : ClassDictionaryBase
{
    public Mask()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-linear-", new ClassDefinition
                {
                    UsesAlphaNumber = true,
                    UsesInteger = true,
                    Template =
                        """
                        --sf-mask-linear-position: {0}deg;
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-position: {0};
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "-mask-linear-", new ClassDefinition
                {
                    UsesAlphaNumber = true,
                    UsesInteger = true,
                    Template =
                        """
                        --sf-mask-linear-position: -{0}deg;
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-position: calc({0} * -1);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-radial-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    UsesDimensionLength = true,
                    Template =
                        """
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops, var(--sf-mask-radial-size)));
                        --sf-mask-radial-size: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}