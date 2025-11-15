namespace Sfumato.Tests.UtilityClasses.Layout;

public class VisibilityTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Visibility()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "visible",
                EscapedClassName = ".visible",
                Styles =
                    """
                    visibility: visible;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invisible",
                EscapedClassName = ".invisible",
                Styles =
                    """
                    visibility: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "collapse",
                EscapedClassName = ".collapse",
                Styles =
                    """
                    visibility: collapse;
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
