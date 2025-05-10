namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class InsetXTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetX()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-x-auto",
                EscapedClassName = ".inset-x-auto",
                Styles =
                    """
                    inset-inline: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-px",
                EscapedClassName = ".inset-x-px",
                Styles =
                    """
                    inset-inline: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-3/4",
                EscapedClassName = @".inset-x-3\/4",
                Styles =
                    """
                    inset-inline: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-x-3/4",
                EscapedClassName = @".-inset-x-3\/4",
                Styles =
                    """
                    inset-inline: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-5",
                EscapedClassName = ".inset-x-5",
                Styles =
                    """
                    inset-inline: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-x-5",
                EscapedClassName = ".-inset-x-5",
                Styles =
                    """
                    inset-inline: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-[1.25rem]",
                EscapedClassName = @".inset-x-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-x-[1.25rem]",
                EscapedClassName = @".-inset-x-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-(--my-inset-x)",
                EscapedClassName = @".inset-x-\(--my-inset-x\)",
                Styles =
                    """
                    inset-inline: var(--my-inset-x);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-(length:--my-inset-x)",
                EscapedClassName = @".inset-x-\(length\:--my-inset-x\)",
                Styles =
                    """
                    inset-inline: var(--my-inset-x);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-[var(--my-inset-x)]",
                EscapedClassName = @".inset-x-\[var\(--my-inset-x\)\]",
                Styles =
                    """
                    inset-inline: var(--my-inset-x);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-x-[length:var(--my-inset-x)]",
                EscapedClassName = @".inset-x-\[length\:var\(--my-inset-x\)\]",
                Styles =
                    """
                    inset-inline: var(--my-inset-x);
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
