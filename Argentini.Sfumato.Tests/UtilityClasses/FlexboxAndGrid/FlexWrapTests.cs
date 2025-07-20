namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexWrapTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void FlexWrap()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "flex-nowrap",
                EscapedClassName = ".flex-nowrap",
                Styles =
                    """
                    flex-wrap: nowrap;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-wrap",
                EscapedClassName = ".flex-wrap",
                Styles =
                    """
                    flex-wrap: wrap;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-wrap-reverse",
                EscapedClassName = ".flex-wrap-reverse",
                Styles =
                    """
                    flex-wrap: wrap-reverse;
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
