namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexGrowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FlexGrow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "grow",
                EscapedClassName = ".grow",
                Styles =
                    """
                    flex-grow: 1;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-2",
                EscapedClassName = ".grow-2",
                Styles =
                    """
                    flex-grow: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-[3]",
                EscapedClassName = @".grow-\[3\]",
                Styles =
                    """
                    flex-grow: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-[0.6]",
                EscapedClassName = @".grow-\[0\.6\]",
                Styles =
                    """
                    flex-grow: 0.6;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-(--my-flex)",
                EscapedClassName = @".grow-\(--my-flex\)",
                Styles =
                    """
                    flex-grow: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-(integer:--my-flex)",
                EscapedClassName = @".grow-\(integer\:--my-flex\)",
                Styles =
                    """
                    flex-grow: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-(number:--my-flex)",
                EscapedClassName = @".grow-\(number\:--my-flex\)",
                Styles =
                    """
                    flex-grow: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-[var(--my-flex)]",
                EscapedClassName = @".grow-\[var\(--my-flex\)\]",
                Styles =
                    """
                    flex-grow: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-[integer:var(--my-flex)]",
                EscapedClassName = @".grow-\[integer\:var\(--my-flex\)\]",
                Styles =
                    """
                    flex-grow: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grow-[number:var(--my-flex)]",
                EscapedClassName = @".grow-\[number\:var\(--my-flex\)\]",
                Styles =
                    """
                    flex-grow: var(--my-flex);
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
