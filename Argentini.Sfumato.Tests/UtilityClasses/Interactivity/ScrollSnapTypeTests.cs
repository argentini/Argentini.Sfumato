namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollSnapTypeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ScrollSnapType()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "snap-none",
                EscapedClassName = ".snap-none",
                Styles =
                    """
                    scroll-snap-type: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "snap-proximity",
                EscapedClassName = ".snap-proximity",
                Styles =
                    """
                    --sf-scroll-snap-strictness: proximity;
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
