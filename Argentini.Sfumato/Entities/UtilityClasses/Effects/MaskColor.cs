// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

public sealed class MaskColor : ClassDictionaryBase
{
    public MaskColor()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-linear-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-from-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-linear-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-to-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-t-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-t-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-r-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-r-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                }
            },
            {
                "mask-b-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-b-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-l-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-l-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-color: {0};
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-y-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-position: {0};
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-y-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-color: {0};
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-x-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-color: {0};
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-x-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-color: {0};
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-radial-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-from-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-radial-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-to-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-conic-from-", new ClassDefinition
                {
                    SelectorSort = 2,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-from-color: {0};

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                }
            },
            {
                "mask-conic-to-", new ClassDefinition
                {
                    SelectorSort = 3,
                    InColorCollection = true,
                    Template =
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-to-color: {0};
                        
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