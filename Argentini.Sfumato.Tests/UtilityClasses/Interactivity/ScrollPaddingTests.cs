namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollPaddingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ScrollPadding()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scroll-p-4",
                EscapedClassName = ".scroll-p-4",
                Styles =
                    """
                    scroll-padding: calc(var(--spacing) * 4);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-p-1/2",
                EscapedClassName = @".scroll-p-1\/2",
                Styles =
                    """
                    scroll-padding: 50%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-p-[1.25rem]",
                EscapedClassName = @".scroll-p-\[1\.25rem\]",
                Styles =
                    """
                    scroll-padding: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-p-[var(--py-padding)]",
                EscapedClassName = @".scroll-p-\[var\(--py-padding\)\]",
                Styles =
                    """
                    scroll-padding: var(--py-padding);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-p-(--py-padding)",
                EscapedClassName = @".scroll-p-\(--py-padding\)",
                Styles =
                    """
                    scroll-padding: var(--py-padding);
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
