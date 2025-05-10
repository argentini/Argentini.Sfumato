namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class LineHeightTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void LineHeight()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "leading-tight",
                EscapedClassName = ".leading-tight",
                Styles =
                    """
                    line-height: var(--leading-tight);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-none",
                EscapedClassName = ".leading-none",
                Styles =
                    """
                    line-height: 1;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-[1.35rem]",
                EscapedClassName = @".leading-\[1\.35rem\]",
                Styles =
                    """
                    line-height: 1.35rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-leading-[1.35rem]",
                EscapedClassName = @".-leading-\[1\.35rem\]",
                Styles =
                    """
                    line-height: -1.35rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-[var(--my-line-height)]",
                EscapedClassName = @".leading-\[var\(--my-line-height\)\]",
                Styles =
                    """
                    line-height: var(--my-line-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-[number:var(--my-line-height)]",
                EscapedClassName = @".leading-\[number\:var\(--my-line-height\)\]",
                Styles =
                    """
                    line-height: var(--my-line-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-(--my-line-height)",
                EscapedClassName = @".leading-\(--my-line-height\)",
                Styles =
                    """
                    line-height: var(--my-line-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "leading-(number:--my-line-height)",
                EscapedClassName = @".leading-\(number\:--my-line-height\)",
                Styles =
                    """
                    line-height: var(--my-line-height);
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
