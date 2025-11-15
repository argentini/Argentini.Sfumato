namespace Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollSnapAlignTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ScrollSnapAlign()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "snap-start",
                EscapedClassName = ".snap-start",
                Styles =
                    """
                    scroll-snap-align: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "snap-align-none",
                EscapedClassName = ".snap-align-none",
                Styles =
                    """
                    scroll-snap-align: none;
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
