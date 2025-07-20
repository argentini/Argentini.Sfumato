namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class MinHeightTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MinHeight()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "min-h-px",
                EscapedClassName = ".min-h-px",
                Styles =
                    """
                    min-height: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-fit",
                EscapedClassName = ".min-h-fit",
                Styles =
                    """
                    min-height: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-lh",
                EscapedClassName = ".min-h-lh",
                Styles =
                    """
                    min-height: 1lh;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-3/4",
                EscapedClassName = @".min-h-3\/4",
                Styles =
                    """
                    min-height: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-5",
                EscapedClassName = ".min-h-5",
                Styles =
                    """
                    min-height: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-[1.25rem]",
                EscapedClassName = @".min-h-\[1\.25rem\]",
                Styles =
                    """
                    min-height: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-(--my-width)",
                EscapedClassName = @".min-h-\(--my-width\)",
                Styles =
                    """
                    min-height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-(length:--my-width)",
                EscapedClassName = @".min-h-\(length\:--my-width\)",
                Styles =
                    """
                    min-height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-[var(--my-width)]",
                EscapedClassName = @".min-h-\[var\(--my-width\)\]",
                Styles =
                    """
                    min-height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-h-[length:var(--my-width)]",
                EscapedClassName = @".min-h-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    min-height: var(--my-width);
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
