namespace Argentini.Sfumato.Tests.UtilityClasses.Svg;

public class StrokeWidthTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void StrokeWidth()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "stroke-2",
                EscapedClassName = ".stroke-2",
                Styles =
                    """
                    stroke-width: 2px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-[1.25rem]",
                EscapedClassName = @".stroke-\[1\.25rem\]",
                Styles =
                    """
                    stroke-width: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "stroke-(length:--my-line)",
                EscapedClassName = @".stroke-\(length\:--my-line\)",
                Styles =
                    """
                    stroke-width: var(--my-line);
                    """,
                IsValid = true,
                IsImportant = false,
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
