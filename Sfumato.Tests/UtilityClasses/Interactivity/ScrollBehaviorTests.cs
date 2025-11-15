namespace Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollBehaviorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ScrollBehavior()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scroll-auto",
                EscapedClassName = ".scroll-auto",
                Styles =
                    """
                    scroll-behavior: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-smooth",
                EscapedClassName = ".scroll-smooth",
                Styles =
                    """
                    scroll-behavior: smooth;
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
