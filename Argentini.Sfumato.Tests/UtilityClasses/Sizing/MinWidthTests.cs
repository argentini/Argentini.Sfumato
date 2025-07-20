namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class MinWidthTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void MinWidth()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "min-w-auto",
                EscapedClassName = ".min-w-auto",
                Styles =
                    """
                    min-width: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-px",
                EscapedClassName = ".min-w-px",
                Styles =
                    """
                    min-width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-fit",
                EscapedClassName = ".min-w-fit",
                Styles =
                    """
                    min-width: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-xl",
                EscapedClassName = ".min-w-xl",
                Styles =
                    """
                    min-width: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-3/4",
                EscapedClassName = @".min-w-3\/4",
                Styles =
                    """
                    min-width: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-5",
                EscapedClassName = ".min-w-5",
                Styles =
                    """
                    min-width: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-[1.25rem]",
                EscapedClassName = @".min-w-\[1\.25rem\]",
                Styles =
                    """
                    min-width: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-(--my-width)",
                EscapedClassName = @".min-w-\(--my-width\)",
                Styles =
                    """
                    min-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-(length:--my-width)",
                EscapedClassName = @".min-w-\(length\:--my-width\)",
                Styles =
                    """
                    min-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-[var(--my-width)]",
                EscapedClassName = @".min-w-\[var\(--my-width\)\]",
                Styles =
                    """
                    min-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "min-w-[length:var(--my-width)]",
                EscapedClassName = @".min-w-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    min-width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
