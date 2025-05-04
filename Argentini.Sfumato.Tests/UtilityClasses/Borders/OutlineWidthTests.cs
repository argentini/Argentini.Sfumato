namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class OutlineWidthTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void OutlineWidth()
    {
        var appRunner = new AppRunner(new AppState());

        var testClasses = new List<TestClass>()
        {
            new()
            {
                ClassName = "outline",
                EscapedClassName = ".outline",
                Styles =
                    """
                    outline-width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-0",
                EscapedClassName = ".outline-0",
                Styles =
                    """
                    outline-width: 0px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-4",
                EscapedClassName = ".outline-4",
                Styles =
                    """
                    outline-width: 4px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[0.75rem]",
                EscapedClassName = @".outline-\[0\.75rem\]",
                Styles =
                    """
                    outline-width: 0.75rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-[length:var(--my-border)]",
                EscapedClassName = @".outline-\[length\:var\(--my-border\)\]",
                Styles = 
                    """
                    outline-width: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-(length:--my-border)",
                EscapedClassName = @".outline-\(length\:--my-border\)",
                Styles = 
                    """
                    outline-width: var(--my-border);
                    """,
                IsValid = true,
                IsImportant = false,
            }
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
