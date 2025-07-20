namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class DivideColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void DivideColor()
    {
        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "divide-current",
                EscapedClassName = ".divide-current",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: currentColor;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-lime-500",
                EscapedClassName = ".divide-lime-500",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: var(--color-lime-500);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-lime-500/25",
                EscapedClassName = @".divide-lime-500\/25",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-[#aabbcc]",
                EscapedClassName = @".divide-\[\#aabbcc\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: #aabbcc;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-[#aabbcc]/25",
                EscapedClassName = @".divide-\[\#aabbcc\]\/25",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: rgba(170,187,204,0.25);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-[color:var(--my-color)]",
                EscapedClassName = @".divide-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: var(--my-color);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-(color:--my-color)",
                EscapedClassName = @".divide-\(color\:--my-color\)",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-color: var(--my-color);
                    }
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
