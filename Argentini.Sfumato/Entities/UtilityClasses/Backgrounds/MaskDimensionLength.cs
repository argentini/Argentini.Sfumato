// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class MaskDimensionLength : ClassDictionaryBase
{
    public MaskDimensionLength()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-linear-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-from-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear-stops", "--sf-mask-linear-position", "--sf-mask-linear-from-color", "--sf-mask-linear-to-color", "--sf-mask-linear-to-position", "--sf-mask-linear", "--sf-mask-linear-from-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-linear-to-", new ClassDefinition
                {
                    SelectorSort = 2,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-to-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear-stops", "--sf-mask-linear-position", "--sf-mask-linear-from-color", "--sf-mask-linear-from-position", "--sf-mask-linear-to-color", "--sf-mask-linear", "--sf-mask-linear-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-t-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-top-from-color", "--sf-mask-top-to-color", "--sf-mask-top-to-position", "--sf-mask-top-from-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-t-to-", new ClassDefinition
                {
                    SelectorSort = 2,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-top-from-position", "--sf-mask-top-from-color", "--sf-mask-top-to-color", "--sf-mask-top-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-r-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-right-from-color", "--sf-mask-right-to-color", "--sf-mask-right-to-position", "--sf-mask-right-from-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-r-to-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect;
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-right-from-color", "--sf-mask-right-from-position", "--sf-mask-right-to-color", "--sf-mask-right-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-b-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-bottom-from-color", "--sf-mask-bottom-to-color", "--sf-mask-bottom-to-position", "--sf-mask-bottom-from-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-b-to-", new ClassDefinition
                {
                    SelectorSort = 2,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-bottom-from-color", "--sf-mask-bottom-from-position", "--sf-mask-bottom-to-color", "--sf-mask-bottom-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-l-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-left-from-color", "--sf-mask-left-from-position", "--sf-mask-left-to-color", "--sf-mask-left-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-l-to-", new ClassDefinition
                {
                    SelectorSort = 2,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-position: calc(var(--spacing) * {0});
                        
                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        
                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-left-from-color", "--sf-mask-left-from-position", "--sf-mask-left-to-color", "--sf-mask-left-to-position", "--spacing", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-y-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-position: calc(var(--spacing) * {0});
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-top-from-color", "--sf-mask-top-from-position", "--sf-mask-top-to-color", "--sf-mask-top-to-position", "--spacing", "--sf-mask-bottom-from-color", "--sf-mask-bottom-from-position", "--sf-mask-bottom-to-color", "--sf-mask-bottom-to-position", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-y-to-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-position: calc(var(--spacing) * {0});
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-top-from-color", "--sf-mask-top-from-position", "--sf-mask-top-to-color", "--sf-mask-top-to-position", "--spacing", "--sf-mask-bottom-from-color", "--sf-mask-bottom-from-position", "--sf-mask-bottom-to-color", "--sf-mask-bottom-to-position", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-x-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-position: calc(var(--spacing) * {0});
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-right-from-color", "--sf-mask-right-from-position", "--sf-mask-right-to-color", "--sf-mask-right-to-position", "--spacing", "--sf-mask-left-from-color", "--sf-mask-left-from-position", "--sf-mask-left-to-color", "--sf-mask-left-to-position", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-x-to-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-position: calc(var(--spacing) * {0});
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-linear", "--sf-mask-left", "--sf-mask-right", "--sf-mask-bottom", "--sf-mask-top", "--sf-mask-right-from-color", "--sf-mask-right-from-position", "--sf-mask-right-to-color", "--sf-mask-right-to-position", "--spacing", "--sf-mask-left-from-color", "--sf-mask-left-from-position", "--sf-mask-left-to-color", "--sf-mask-left-to-position", "--sf-mask-radial", "--sf-mask-conic" ]
                }
            },
            {
                "mask-radial-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-from-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-radial-stops", "--sf-mask-radial-shape", "--sf-mask-radial-size", "--sf-mask-radial-position", "--sf-mask-radial-from-color", "--sf-mask-radial-from-position", "--sf-mask-radial-to-color", "--sf-mask-radial-to-position", "--sf-mask-radial", "--spacing", "--sf-mask-linear", "--sf-mask-conic" ]
                }
            },
            {
                "mask-radial-to-", new ClassDefinition
                {
                    SelectorSort = 1,
                    UsesDimensionLength = true,
                    UsesSpacing = true,
                    Template =
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-to-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    UsesCssCustomProperties = [ "--sf-mask-radial-stops", "--sf-mask-radial-shape", "--sf-mask-radial-size", "--sf-mask-radial-position", "--sf-mask-radial-from-color", "--sf-mask-radial-from-position", "--sf-mask-radial-to-color", "--sf-mask-radial-to-position", "--sf-mask-radial", "--spacing", "--sf-mask-linear", "--sf-mask-conic" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}