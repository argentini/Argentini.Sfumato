namespace Sfumato.Tests.UtilityClasses.Borders;

public class OutlineColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void OutlineColor()
    {
        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "outline-current",
                EscapedClassName = ".outline-current",
                Styles =
                    """
                    outline-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-lime-500",
                EscapedClassName = ".outline-lime-500",
                Styles =
                    """
                    outline-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-lime-500/25",
                EscapedClassName = @".outline-lime-500\/25",
                Styles =
                    """
                    outline-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[#aabbcc]",
                EscapedClassName = @".outline-\[\#aabbcc\]",
                Styles =
                    """
                    outline-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[#aabbcc]/25",
                EscapedClassName = @".outline-\[\#aabbcc\]\/25",
                Styles =
                    """
                    outline-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[color:var(--my-color)]",
                EscapedClassName = @".outline-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    outline-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-(color:--my-color)",
                EscapedClassName = @".outline-\(color\:--my-color\)",
                Styles =
                    """
                    outline-color: var(--my-color);
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
