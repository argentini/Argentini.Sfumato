namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class InsetEndTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetEnd()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "end-auto",
                EscapedClassName = ".end-auto",
                Styles =
                    """
                    inset-inline-end: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-px",
                EscapedClassName = ".end-px",
                Styles =
                    """
                    inset-inline-end: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-3/4",
                EscapedClassName = @".end-3\/4",
                Styles =
                    """
                    inset-inline-end: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-end-3/4",
                EscapedClassName = @".-end-3\/4",
                Styles =
                    """
                    inset-inline-end: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-5",
                EscapedClassName = ".end-5",
                Styles =
                    """
                    inset-inline-end: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-end-5",
                EscapedClassName = ".-end-5",
                Styles =
                    """
                    inset-inline-end: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-[1.25rem]",
                EscapedClassName = @".end-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline-end: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-end-[1.25rem]",
                EscapedClassName = @".-end-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline-end: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-(--my-top)",
                EscapedClassName = @".end-\(--my-top\)",
                Styles =
                    """
                    inset-inline-end: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-(length:--my-top)",
                EscapedClassName = @".end-\(length\:--my-top\)",
                Styles =
                    """
                    inset-inline-end: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-[var(--my-top)]",
                EscapedClassName = @".end-\[var\(--my-top\)\]",
                Styles =
                    """
                    inset-inline-end: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "end-[length:var(--my-top)]",
                EscapedClassName = @".end-\[length\:var\(--my-top\)\]",
                Styles =
                    """
                    inset-inline-end: var(--my-top);
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
