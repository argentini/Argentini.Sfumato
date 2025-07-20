namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class ZindexTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Zindex()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "z-auto",
                EscapedClassName = ".z-auto",
                Styles =
                    """
                    z-index: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-10",
                EscapedClassName = ".z-10",
                Styles =
                    """
                    z-index: 10;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-[25]",
                EscapedClassName = @".z-\[25\]",
                Styles =
                    """
                    z-index: 25;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-z-[25]",
                EscapedClassName = @".-z-\[25\]",
                Styles =
                    """
                    z-index: -25;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-(--my-z)",
                EscapedClassName = @".z-\(--my-z\)",
                Styles =
                    """
                    z-index: var(--my-z);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-(integer:--my-z)",
                EscapedClassName = @".z-\(integer\:--my-z\)",
                Styles =
                    """
                    z-index: var(--my-z);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-[var(--my-z)]",
                EscapedClassName = @".z-\[var\(--my-z\)\]",
                Styles =
                    """
                    z-index: var(--my-z);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "z-[integer:var(--my-z)]",
                EscapedClassName = @".z-\[integer\:var\(--my-z\)\]",
                Styles =
                    """
                    z-index: var(--my-z);
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
