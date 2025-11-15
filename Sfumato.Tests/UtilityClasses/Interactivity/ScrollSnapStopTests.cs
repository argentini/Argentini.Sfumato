namespace Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollSnapStopTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ScrollSnapStop()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "snap-normal",
                EscapedClassName = ".snap-normal",
                Styles =
                    """
                    scroll-snap-stop: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "snap-always",
                EscapedClassName = ".snap-always",
                Styles =
                    """
                    scroll-snap-stop: always;
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
