namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class HeightTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Height()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "h-auto",
                EscapedClassName = ".h-auto",
                Styles =
                    """
                    height: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-px",
                EscapedClassName = ".h-px",
                Styles =
                    """
                    height: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-fit",
                EscapedClassName = ".h-fit",
                Styles =
                    """
                    height: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-lh",
                EscapedClassName = ".h-lh",
                Styles =
                    """
                    height: 1lh;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-3/4",
                EscapedClassName = @".h-3\/4",
                Styles =
                    """
                    height: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-5",
                EscapedClassName = ".h-5",
                Styles =
                    """
                    height: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-[1.25rem]",
                EscapedClassName = @".h-\[1\.25rem\]",
                Styles =
                    """
                    height: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-(--my-height)",
                EscapedClassName = @".h-\(--my-height\)",
                Styles =
                    """
                    height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-(length:--my-height)",
                EscapedClassName = @".h-\(length\:--my-height\)",
                Styles =
                    """
                    height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-[var(--my-height)]",
                EscapedClassName = @".h-\[var\(--my-height\)\]",
                Styles =
                    """
                    height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "h-[length:var(--my-height)]",
                EscapedClassName = @".h-\[length\:var\(--my-height\)\]",
                Styles =
                    """
                    height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tabp:h-[calc(100vh_-_6rem)]",
                EscapedClassName = @".tabp\:h-\[calc\(100vh_-_6rem\)\]",
                Styles =
                    """
                    height: calc(100vh - 6rem);
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
