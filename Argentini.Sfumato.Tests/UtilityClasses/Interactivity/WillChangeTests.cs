namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class WillChangeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void WillChange()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "will-change-auto",
                EscapedClassName = ".will-change-auto",
                Styles =
                    """
                    will-change: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "will-change-transform",
                EscapedClassName = ".will-change-transform",
                Styles =
                    """
                    will-change: transform;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "will-change-[top,left]",
                EscapedClassName = @".will-change-\[top\,left\]",
                Styles =
                    """
                    will-change: top,left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "will-change-(--my-value)",
                EscapedClassName = @".will-change-\(--my-value\)",
                Styles =
                    """
                    will-change: var(--my-value);
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
