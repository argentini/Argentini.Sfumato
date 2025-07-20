namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class RightTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Right()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "right-auto",
                EscapedClassName = ".right-auto",
                Styles =
                    """
                    right: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-px",
                EscapedClassName = ".right-px",
                Styles =
                    """
                    right: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-3/4",
                EscapedClassName = @".right-3\/4",
                Styles =
                    """
                    right: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-right-3/4",
                EscapedClassName = @".-right-3\/4",
                Styles =
                    """
                    right: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-5",
                EscapedClassName = ".right-5",
                Styles =
                    """
                    right: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-right-5",
                EscapedClassName = ".-right-5",
                Styles =
                    """
                    right: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-[1.25rem]",
                EscapedClassName = @".right-\[1\.25rem\]",
                Styles =
                    """
                    right: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-right-[1.25rem]",
                EscapedClassName = @".-right-\[1\.25rem\]",
                Styles =
                    """
                    right: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-(--my-right)",
                EscapedClassName = @".right-\(--my-right\)",
                Styles =
                    """
                    right: var(--my-right);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-(length:--my-right)",
                EscapedClassName = @".right-\(length\:--my-right\)",
                Styles =
                    """
                    right: var(--my-right);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-[var(--my-right)]",
                EscapedClassName = @".right-\[var\(--my-right\)\]",
                Styles =
                    """
                    right: var(--my-right);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "right-[length:var(--my-right)]",
                EscapedClassName = @".right-\[length\:var\(--my-right\)\]",
                Styles =
                    """
                    right: var(--my-right);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
