namespace Argentini.Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class TransitionDurationTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void TransitionDuration()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "duration-initial",
                EscapedClassName = ".duration-initial",
                Styles =
                    """
                    --sf-duration: initial;
                    transition-duration: initial;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "duration-100",
                EscapedClassName = ".duration-100",
                Styles =
                    """
                    --sf-duration: 100ms;
                    transition-duration: 100ms;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "duration-[150ms]",
                EscapedClassName = @".duration-\[150ms\]",
                Styles =
                    """
                    --sf-duration: 150ms;
                    transition-duration: 150ms;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "duration-(--my-duration)",
                EscapedClassName = @".duration-\(--my-duration\)",
                Styles =
                    """
                    --sf-duration: var(--my-duration);
                    transition-duration: var(--my-duration);
                    """,
                IsValid = true,
                IsImportant = false,
            },
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
