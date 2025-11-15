namespace Sfumato.Tests.UtilityClasses.Layout;

public class BreakBeforeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BreakBefore()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "break-before-auto",
                EscapedClassName = ".break-before-auto",
                Styles =
                    """
                    break-before: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-before-avoid",
                EscapedClassName = ".break-before-avoid",
                Styles =
                    """
                    break-before: avoid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-before-avoid-page",
                EscapedClassName = ".break-before-avoid-page",
                Styles =
                    """
                    break-before: avoid-page;
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
