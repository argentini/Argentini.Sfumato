namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class InsetTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Inset()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-auto",
                EscapedClassName = ".inset-auto",
                Styles =
                    """
                    inset: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-px",
                EscapedClassName = ".inset-px",
                Styles =
                    """
                    inset: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-3/4",
                EscapedClassName = @".inset-3\/4",
                Styles =
                    """
                    inset: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-3/4",
                EscapedClassName = @".-inset-3\/4",
                Styles =
                    """
                    inset: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-5",
                EscapedClassName = ".inset-5",
                Styles =
                    """
                    inset: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-5",
                EscapedClassName = ".-inset-5",
                Styles =
                    """
                    inset: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-[1.25rem]",
                EscapedClassName = @".inset-\[1\.25rem\]",
                Styles =
                    """
                    inset: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-inset-[1.25rem]",
                EscapedClassName = @".-inset-\[1\.25rem\]",
                Styles =
                    """
                    inset: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-(--my-inset)",
                EscapedClassName = @".inset-\(--my-inset\)",
                Styles =
                    """
                    inset: var(--my-inset);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-(length:--my-inset)",
                EscapedClassName = @".inset-\(length\:--my-inset\)",
                Styles =
                    """
                    inset: var(--my-inset);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-[var(--my-inset)]",
                EscapedClassName = @".inset-\[var\(--my-inset\)\]",
                Styles =
                    """
                    inset: var(--my-inset);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-[length:var(--my-inset)]",
                EscapedClassName = @".inset-\[length\:var\(--my-inset\)\]",
                Styles =
                    """
                    inset: var(--my-inset);
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
