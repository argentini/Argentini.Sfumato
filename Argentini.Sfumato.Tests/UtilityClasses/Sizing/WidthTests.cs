namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class WidthTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Width()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "w-auto",
                EscapedClassName = ".w-auto",
                Styles =
                    """
                    width: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-px",
                EscapedClassName = ".w-px",
                Styles =
                    """
                    width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-fit",
                EscapedClassName = ".w-fit",
                Styles =
                    """
                    width: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-xl",
                EscapedClassName = ".w-xl",
                Styles =
                    """
                    width: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-3/4",
                EscapedClassName = @".w-3\/4",
                Styles =
                    """
                    width: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-5",
                EscapedClassName = ".w-5",
                Styles =
                    """
                    width: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-[1.25rem]",
                EscapedClassName = @".w-\[1\.25rem\]",
                Styles =
                    """
                    width: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-(--my-width)",
                EscapedClassName = @".w-\(--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-(length:--my-width)",
                EscapedClassName = @".w-\(length\:--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-[var(--my-width)]",
                EscapedClassName = @".w-\[var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-[length:var(--my-width)]",
                EscapedClassName = @".w-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
