namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class BreakAfterTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BreakAfter()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "break-after-auto",
                EscapedClassName = ".break-after-auto",
                Styles =
                    """
                    break-after: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-after-avoid",
                EscapedClassName = ".break-after-avoid",
                Styles =
                    """
                    break-after: avoid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-after-avoid-page",
                EscapedClassName = ".break-after-avoid-page",
                Styles =
                    """
                    break-after: avoid-page;
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
