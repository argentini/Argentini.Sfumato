namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundRepeatTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundRepeat()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-repeat",
                EscapedClassName = ".bg-repeat",
                Styles =
                    """
                    background-repeat: repeat;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-repeat-x",
                EscapedClassName = ".bg-repeat-x",
                Styles =
                    """
                    background-repeat: repeat-x;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-no-repeat",
                EscapedClassName = ".bg-no-repeat",
                Styles =
                    """
                    background-repeat: no-repeat;
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
