namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class TouchActionTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TouchAction()
    {
        var appRunner = new AppRunner(new AppState());
        
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
