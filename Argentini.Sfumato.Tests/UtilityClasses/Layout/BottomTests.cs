namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class BottomTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Bottom()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bottom-auto",
                EscapedClassName = ".bottom-auto",
                Styles =
                    """
                    bottom: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-px",
                EscapedClassName = ".bottom-px",
                Styles =
                    """
                    bottom: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-3/4",
                EscapedClassName = @".bottom-3\/4",
                Styles =
                    """
                    bottom: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-bottom-3/4",
                EscapedClassName = @".-bottom-3\/4",
                Styles =
                    """
                    bottom: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-5",
                EscapedClassName = ".bottom-5",
                Styles =
                    """
                    bottom: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-bottom-5",
                EscapedClassName = ".-bottom-5",
                Styles =
                    """
                    bottom: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-[1.25rem]",
                EscapedClassName = @".bottom-\[1\.25rem\]",
                Styles =
                    """
                    bottom: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-bottom-[1.25rem]",
                EscapedClassName = @".-bottom-\[1\.25rem\]",
                Styles =
                    """
                    bottom: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-(--my-bottom)",
                EscapedClassName = @".bottom-\(--my-bottom\)",
                Styles =
                    """
                    bottom: var(--my-bottom);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-(length:--my-bottom)",
                EscapedClassName = @".bottom-\(length\:--my-bottom\)",
                Styles =
                    """
                    bottom: var(--my-bottom);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-[var(--my-bottom)]",
                EscapedClassName = @".bottom-\[var\(--my-bottom\)\]",
                Styles =
                    """
                    bottom: var(--my-bottom);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bottom-[length:var(--my-bottom)]",
                EscapedClassName = @".bottom-\[length\:var\(--my-bottom\)\]",
                Styles =
                    """
                    bottom: var(--my-bottom);
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
