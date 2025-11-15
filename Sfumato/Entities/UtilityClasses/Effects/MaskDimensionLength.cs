// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Effects;

public sealed class MaskDimensionLength : ClassDictionaryBase
{
    public MaskDimensionLength()
    {
        Group = "mask-image";
        Description = "Utilities for setting custom mask dimension lengths.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "mask-linear-from-", new ClassDefinition
                {
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                        --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                        --sf-mask-linear-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-from-position: {0};
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                        --sf-mask-top-to-position: {0};
                        --sf-mask-bottom: linear-gradient(to bottom, var(--sf-mask-bottom-from-color) var(--sf-mask-bottom-from-position), var(--sf-mask-bottom-to-color) var(--sf-mask-bottom-to-position));
                        --sf-mask-bottom-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-from-position: {0};
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                        --sf-mask-right: linear-gradient(to right, var(--sf-mask-right-from-color) var(--sf-mask-right-from-position), var(--sf-mask-right-to-color) var(--sf-mask-right-to-position));
                        --sf-mask-right-to-position: {0};
                        --sf-mask-left: linear-gradient(to left, var(--sf-mask-left-from-color) var(--sf-mask-left-from-position), var(--sf-mask-left-to-color) var(--sf-mask-left-to-position));
                        --sf-mask-left-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
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
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-radial-stops: var(--sf-mask-radial-shape) var(--sf-mask-radial-size) at var(--sf-mask-radial-position), var(--sf-mask-radial-from-color) var(--sf-mask-radial-from-position), var(--sf-mask-radial-to-color) var(--sf-mask-radial-to-position);
                        --sf-mask-radial: radial-gradient(var(--sf-mask-radial-stops));
                        --sf-mask-radial-to-position: {0};

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
                    SelectorSort = 1,
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-from-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-from-position: {0};

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
                    SelectorSort = 2,
                    InLengthCollection = true,
                    Template =
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-to-position: calc(var(--spacing) * {0});

                        -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                        mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                        -webkit-mask-composite: source-in;
                        mask-composite: intersect
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        --sf-mask-conic-stops: from var(--sf-mask-conic-position), var(--sf-mask-conic-from-color) var(--sf-mask-conic-from-position), var(--sf-mask-conic-to-color) var(--sf-mask-conic-to-position);
                        --sf-mask-conic: conic-gradient(var(--sf-mask-conic-stops));
                        --sf-mask-conic-to-position: {0};

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