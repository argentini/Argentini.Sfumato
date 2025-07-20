namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class SizeTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Size()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "size-auto",
                EscapedClassName = ".size-auto",
                Styles =
                    """
                    width: auto;
                    height: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-px",
                EscapedClassName = ".size-px",
                Styles =
                    """
                    width: 1px;
                    height: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-fit",
                EscapedClassName = ".size-fit",
                Styles =
                    """
                    width: fit-content;
                    height: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-xl",
                EscapedClassName = ".size-xl",
                Styles =
                    """
                    width: var(--container-xl);
                    height: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-3/4",
                EscapedClassName = @".size-3\/4",
                Styles =
                    """
                    width: 75%;
                    height: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-5",
                EscapedClassName = ".size-5",
                Styles =
                    """
                    width: calc(var(--spacing) * 5);
                    height: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-[1.25rem]",
                EscapedClassName = @".size-\[1\.25rem\]",
                Styles =
                    """
                    width: 1.25rem;
                    height: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-(--my-width)",
                EscapedClassName = @".size-\(--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-(length:--my-width)",
                EscapedClassName = @".size-\(length\:--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-[var(--my-width)]",
                EscapedClassName = @".size-\[var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-[length:var(--my-width)]",
                EscapedClassName = @".size-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
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
