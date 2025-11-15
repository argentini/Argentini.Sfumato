namespace Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class TransitionDelayTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TransitionDelay()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "delay-100",
                EscapedClassName = ".delay-100",
                Styles =
                    """
                    transition-delay: 100ms;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "delay-[150ms]",
                EscapedClassName = @".delay-\[150ms\]",
                Styles =
                    """
                    transition-delay: 150ms;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "delay-(--my-duration)",
                EscapedClassName = @".delay-\(--my-duration\)",
                Styles =
                    """
                    transition-delay: var(--my-duration);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
