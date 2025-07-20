namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class TouchActionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void TouchAction()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "touch-auto",
                EscapedClassName = ".touch-auto",
                Styles =
                    """
                    touch-action: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "touch-pan-y",
                EscapedClassName = ".touch-pan-y",
                Styles =
                    """
                    --sf-pan-y: pan-y;
                    touch-action: var(--sf-pan-x) var(--sf-pan-y) var(--sf-pinch-zoom);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "touch-manipulation",
                EscapedClassName = ".touch-manipulation",
                Styles =
                    """
                    touch-action: manipulation;
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
