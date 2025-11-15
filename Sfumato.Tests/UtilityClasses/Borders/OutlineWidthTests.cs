namespace Sfumato.Tests.UtilityClasses.Borders;

public class OutlineWidthTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void OutlineWidth()
    {
        var testClasses = new List<TestClass>()
        {
            new()
            {
                ClassName = "outline",
                EscapedClassName = ".outline",
                Styles =
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-0",
                EscapedClassName = ".outline-0",
                Styles =
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: 0px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-4",
                EscapedClassName = ".outline-4",
                Styles =
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: 4px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-4.5",
                EscapedClassName = @".outline-4\.5",
                Styles =
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: 4.5px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[0.75rem]",
                EscapedClassName = @".outline-\[0\.75rem\]",
                Styles =
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: 0.75rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[length:var(--my-border)]",
                EscapedClassName = @".outline-\[length\:var\(--my-border\)\]",
                Styles = 
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-(length:--my-border)",
                EscapedClassName = @".outline-\(length\:--my-border\)",
                Styles = 
                    """
                    outline-style: var(--sf-outline-style);
                    outline-width: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            }
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
