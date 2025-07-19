namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskLinearTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void MaskLinear()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            #region mask-linear
            
            new ()
            {
                ClassName = "mask-linear-50",
                EscapedClassName = ".mask-linear-50",
                Styles =
                    """
                    --sf-mask-linear-position: 50deg;
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-mask-linear-50",
                EscapedClassName = ".-mask-linear-50",
                Styles =
                    """
                    --sf-mask-linear-position: -50deg;
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-[37.5deg]",
                EscapedClassName = @".mask-linear-\[37\.5deg\]",
                Styles =
                    """
                    --sf-mask-linear-position: 37.5deg;
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-(--my-angle)",
                EscapedClassName = @".mask-linear-\(--my-angle\)",
                Styles =
                    """
                    --sf-mask-linear-position: var(--my-angle);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-(angle:--my-angle)",
                EscapedClassName = @".mask-linear-\(angle\:--my-angle\)",
                Styles =
                    """
                    --sf-mask-linear-position: var(--my-angle);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops, var(--sf-mask-linear-position)));

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region mask-linear-from

            new ()
            {
                ClassName = "mask-linear-from-5",
                EscapedClassName = ".mask-linear-from-5",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-position: calc(var(--spacing) * 5);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-[1.25rem]",
                EscapedClassName = @".mask-linear-from-\[1\.25rem\]",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-position: 1.25rem;

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-(length:--my-length)",
                EscapedClassName = @".mask-linear-from-\(length\:--my-length\)",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-position: var(--my-length);

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-37%",
                EscapedClassName = @".mask-linear-from-37\%",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-position: 37%;

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-[37.5%]",
                EscapedClassName = @".mask-linear-from-\[37\.5\%\]",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-position: 37.5%;

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-lime-500",
                EscapedClassName = ".mask-linear-from-lime-500",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-color: var(--color-lime-500);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-lime-500/50",
                EscapedClassName = @".mask-linear-from-lime-500\/50",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-color: color-mix(in oklab, var(--color-lime-500) 50%, transparent);

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-from-(color:--my-color)",
                EscapedClassName = @".mask-linear-from-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-from-color: var(--my-color);

                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);

                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region mask-linear-to

            new ()
            {
                ClassName = "mask-linear-to-5",
                EscapedClassName = ".mask-linear-to-5",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-position: calc(var(--spacing) * 5);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-[1.25rem]",
                EscapedClassName = @".mask-linear-to-\[1\.25rem\]",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-position: 1.25rem;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-(length:--my-length)",
                EscapedClassName = @".mask-linear-to-\(length\:--my-length\)",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-position: var(--my-length);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-37%",
                EscapedClassName = @".mask-linear-to-37\%",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-position: 37%;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-[37.5%]",
                EscapedClassName = @".mask-linear-to-\[37\.5\%\]",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-position: 37.5%;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-lime-500",
                EscapedClassName = ".mask-linear-to-lime-500",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-color: var(--color-lime-500);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-lime-500/50",
                EscapedClassName = @".mask-linear-to-lime-500\/50",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-color: color-mix(in oklab, var(--color-lime-500) 50%, transparent);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-linear-to-(color:--my-color)",
                EscapedClassName = @".mask-linear-to-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-mask-linear-stops: var(--sf-mask-linear-position), var(--sf-mask-linear-from-color) var(--sf-mask-linear-from-position), var(--sf-mask-linear-to-color) var(--sf-mask-linear-to-position);
                    --sf-mask-linear: linear-gradient(var(--sf-mask-linear-stops));
                    --sf-mask-linear-to-color: var(--my-color);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
          
            #region mask-t-from

            new ()
            {
                ClassName = "mask-t-from-5",
                EscapedClassName = ".mask-t-from-5",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-position: calc(var(--spacing) * 5);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-[1.25rem]",
                EscapedClassName = @".mask-t-from-\[1\.25rem\]",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-position: 1.25rem;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-(length:--my-length)",
                EscapedClassName = @".mask-t-from-\(length\:--my-length\)",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-position: var(--my-length);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-37%",
                EscapedClassName = @".mask-t-from-37\%",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-position: 37%;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-[37.5%]",
                EscapedClassName = @".mask-t-from-\[37\.5\%\]",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-position: 37.5%;
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-lime-500",
                EscapedClassName = ".mask-t-from-lime-500",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-color: var(--color-lime-500);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-lime-500/50",
                EscapedClassName = @".mask-t-from-lime-500\/50",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-color: color-mix(in oklab, var(--color-lime-500) 50%, transparent);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-t-from-(color:--my-color)",
                EscapedClassName = @".mask-t-from-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-mask-linear: var(--sf-mask-left), var(--sf-mask-right), var(--sf-mask-bottom), var(--sf-mask-top);
                    --sf-mask-top: linear-gradient(to top, var(--sf-mask-top-from-color) var(--sf-mask-top-from-position), var(--sf-mask-top-to-color) var(--sf-mask-top-to-position));
                    --sf-mask-top-from-color: var(--my-color);
                    
                    -webkit-mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    mask-image: var(--sf-mask-linear), var(--sf-mask-radial), var(--sf-mask-conic);
                    
                    -webkit-mask-composite: source-in;
                    mask-composite: intersect;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
