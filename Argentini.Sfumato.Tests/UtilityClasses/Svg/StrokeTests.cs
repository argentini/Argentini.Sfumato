namespace Argentini.Sfumato.Tests.UtilityClasses.Svg;

public class StrokeTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Stroke()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        appRunner.Library.ColorsByName.Add("fynydd-hex", "#0088ff");
        appRunner.Library.ColorsByName.Add("fynydd-rgb", "rgba(0, 136, 255, 1.0)");
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "stroke-lime-800",
                EscapedClassName = ".stroke-lime-800",
                Styles =
                    """
                    stroke: var(--color-lime-800);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-lime-800/37",
                EscapedClassName = @".stroke-lime-800\/37",
                Styles =
                    """
                    stroke: color-mix(in oklab, var(--color-lime-800) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-fynydd-hex/37",
                EscapedClassName = @".stroke-fynydd-hex\/37",
                Styles =
                    """
                    stroke: color-mix(in srgb, var(--color-fynydd-hex) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-fynydd-rgb/37",
                EscapedClassName = @".stroke-fynydd-rgb\/37",
                Styles =
                    """
                    stroke: color-mix(in srgb, var(--color-fynydd-rgb) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-lime-800!",
                EscapedClassName = @".stroke-lime-800\!",
                Styles =
                    """
                    stroke: var(--color-lime-800) !important;
                    """,
                IsValid = true,
                IsImportant = true,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
