namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class MaxWidthTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaxWidth()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "max-w-auto",
                EscapedClassName = ".max-w-auto",
                Styles =
                    """
                    max-width: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-px",
                EscapedClassName = ".max-w-px",
                Styles =
                    """
                    max-width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-fit",
                EscapedClassName = ".max-w-fit",
                Styles =
                    """
                    max-width: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-xl",
                EscapedClassName = ".max-w-xl",
                Styles =
                    """
                    max-width: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-3/4",
                EscapedClassName = @".max-w-3\/4",
                Styles =
                    """
                    max-width: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-5",
                EscapedClassName = ".max-w-5",
                Styles =
                    """
                    max-width: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-[1.25rem]",
                EscapedClassName = @".max-w-\[1\.25rem\]",
                Styles =
                    """
                    max-width: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-(--my-width)",
                EscapedClassName = @".max-w-\(--my-width\)",
                Styles =
                    """
                    max-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-(length:--my-width)",
                EscapedClassName = @".max-w-\(length\:--my-width\)",
                Styles =
                    """
                    max-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-[var(--my-width)]",
                EscapedClassName = @".max-w-\[var\(--my-width\)\]",
                Styles =
                    """
                    max-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-w-[length:var(--my-width)]",
                EscapedClassName = @".max-w-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    max-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "container",
                EscapedClassName = ".container",
                Styles =
                    """
                    width: 100%;
                    @variant sm { max-width: var(--breakpoint-sm); }
                    @variant md { max-width: var(--breakpoint-md); }
                    @variant lg { max-width: var(--breakpoint-lg); }
                    @variant xl { max-width: var(--breakpoint-xl); }
                    @variant 2xl { max-width: var(--breakpoint-2xl); }
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
