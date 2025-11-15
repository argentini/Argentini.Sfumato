namespace Sfumato.Tests.UtilityClasses.Typography;

public class LetterSpacingTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void LetterSpacing()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "tracking-tighter",
                EscapedClassName = ".tracking-tighter",
                Styles =
                    """
                    --sf-tracking: var(--tracking-tighter);
                    letter-spacing: var(--tracking-tighter);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tracking-[0.25rem]",
                EscapedClassName = @".tracking-\[0\.25rem\]",
                Styles =
                    """
                    letter-spacing: 0.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-tracking-[0.25rem]",
                EscapedClassName = @".-tracking-\[0\.25rem\]",
                Styles =
                    """
                    letter-spacing: -0.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tracking-[var(--my-tracking)]",
                EscapedClassName = @".tracking-\[var\(--my-tracking\)\]",
                Styles =
                    """
                    letter-spacing: var(--my-tracking);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tracking-[length:var(--my-tracking)]",
                EscapedClassName = @".tracking-\[length\:var\(--my-tracking\)\]",
                Styles =
                    """
                    letter-spacing: var(--my-tracking);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tracking-(--my-tracking)",
                EscapedClassName = @".tracking-\(--my-tracking\)",
                Styles =
                    """
                    letter-spacing: var(--my-tracking);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "tracking-(length:--my-tracking)",
                EscapedClassName = @".tracking-\(length\:--my-tracking\)",
                Styles =
                    """
                    letter-spacing: var(--my-tracking);
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
