namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexShrinkTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void FlexShrink()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "shrink",
                EscapedClassName = ".shrink",
                Styles =
                    """
                    flex-shrink: 1;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-2",
                EscapedClassName = ".shrink-2",
                Styles =
                    """
                    flex-shrink: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-[3]",
                EscapedClassName = @".shrink-\[3\]",
                Styles =
                    """
                    flex-shrink: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-[0.6]",
                EscapedClassName = @".shrink-\[0\.6\]",
                Styles =
                    """
                    flex-shrink: 0.6;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-(--my-flex)",
                EscapedClassName = @".shrink-\(--my-flex\)",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-(integer:--my-flex)",
                EscapedClassName = @".shrink-\(integer\:--my-flex\)",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-(number:--my-flex)",
                EscapedClassName = @".shrink-\(number\:--my-flex\)",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-[var(--my-flex)]",
                EscapedClassName = @".shrink-\[var\(--my-flex\)\]",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-[integer:var(--my-flex)]",
                EscapedClassName = @".shrink-\[integer\:var\(--my-flex\)\]",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shrink-[number:var(--my-flex)]",
                EscapedClassName = @".shrink-\[number\:var\(--my-flex\)\]",
                Styles =
                    """
                    flex-shrink: var(--my-flex);
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
