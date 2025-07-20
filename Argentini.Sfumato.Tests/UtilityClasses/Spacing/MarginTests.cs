namespace Argentini.Sfumato.Tests.UtilityClasses.Spacing;

public class MarginTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Margin()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "m-px",
                EscapedClassName = ".m-px",
                Styles =
                    """
                    margin: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-m-px",
                EscapedClassName = ".-m-px",
                Styles =
                    """
                    margin: -1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-3/4",
                EscapedClassName = @".m-3\/4",
                Styles =
                    """
                    margin: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-5",
                EscapedClassName = ".m-5",
                Styles =
                    """
                    margin: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-m-5",
                EscapedClassName = ".-m-5",
                Styles =
                    """
                    margin: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-[1.25rem]",
                EscapedClassName = @".m-\[1\.25rem\]",
                Styles =
                    """
                    margin: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-(--my-margin)",
                EscapedClassName = @".m-\(--my-margin\)",
                Styles =
                    """
                    margin: var(--my-margin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-m-(--my-margin)",
                EscapedClassName = @".-m-\(--my-margin\)",
                Styles =
                    """
                    margin: calc(var(--my-margin) * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-(length:--my-margin)",
                EscapedClassName = @".m-\(length\:--my-margin\)",
                Styles =
                    """
                    margin: var(--my-margin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-[var(--my-margin)]",
                EscapedClassName = @".m-\[var\(--my-margin\)\]",
                Styles =
                    """
                    margin: var(--my-margin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "m-[length:var(--my-margin)]",
                EscapedClassName = @".m-\[length\:var\(--my-margin\)\]",
                Styles =
                    """
                    margin: var(--my-margin);
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
