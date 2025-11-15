namespace Sfumato.Tests.UtilityClasses.Layout;

public class OverscrollTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Overscroll()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "overscroll-auto",
                EscapedClassName = ".overscroll-auto",
                Styles =
                    """
                    overscroll-behavior: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "overscroll-x-auto",
                EscapedClassName = ".overscroll-x-auto",
                Styles =
                    """
                    overscroll-behavior-x: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "overscroll-y-auto",
                EscapedClassName = ".overscroll-y-auto",
                Styles =
                    """
                    overscroll-behavior-y: auto;
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
