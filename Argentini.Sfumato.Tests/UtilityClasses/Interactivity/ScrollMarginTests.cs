namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ScrollMarginTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ScrollMargin()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scroll-m-4",
                EscapedClassName = ".scroll-m-4",
                Styles =
                    """
                    scroll-margin: calc(var(--spacing) * 4);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-m-1/2",
                EscapedClassName = @".scroll-m-1\/2",
                Styles =
                    """
                    scroll-margin: 50%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-m-[1.25rem]",
                EscapedClassName = @".scroll-m-\[1\.25rem\]",
                Styles =
                    """
                    scroll-margin: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-m-[var(--my-margin)]",
                EscapedClassName = @".scroll-m-\[var\(--my-margin\)\]",
                Styles =
                    """
                    scroll-margin: var(--my-margin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scroll-m-(--my-margin)",
                EscapedClassName = @".scroll-m-\(--my-margin\)",
                Styles =
                    """
                    scroll-margin: var(--my-margin);
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
