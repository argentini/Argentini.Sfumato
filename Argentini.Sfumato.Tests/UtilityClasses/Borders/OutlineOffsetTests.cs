namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class OutlineOffsetTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void OutlineOffset()
    {
        var testClasses = new List<TestClass>()
        {
            new()
            {
                ClassName = "outline-offset-4",
                EscapedClassName = ".outline-offset-4",
                Styles =
                    """
                    outline-offset: 4px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "-outline-offset-4",
                EscapedClassName = ".-outline-offset-4",
                Styles =
                    """
                    outline-offset: calc(4px * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-offset-[0.75rem]",
                EscapedClassName = @".outline-offset-\[0\.75rem\]",
                Styles =
                    """
                    outline-offset: 0.75rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "-outline-offset-[0.75rem]",
                EscapedClassName = @".-outline-offset-\[0\.75rem\]",
                Styles =
                    """
                    outline-offset: calc(0.75rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-offset-[length:var(--my-border)]",
                EscapedClassName = @".outline-offset-\[length\:var\(--my-border\)\]",
                Styles = 
                    """
                    outline-offset: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-offset-(length:--my-border)",
                EscapedClassName = @".outline-offset-\(length\:--my-border\)",
                Styles = 
                    """
                    outline-offset: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "-outline-offset-(length:--my-border)",
                EscapedClassName = @".-outline-offset-\(length\:--my-border\)",
                Styles = 
                    """
                    outline-offset: calc(var(--my-border) * -1);
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
