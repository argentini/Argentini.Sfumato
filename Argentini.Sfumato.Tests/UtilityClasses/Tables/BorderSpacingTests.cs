namespace Argentini.Sfumato.Tests.UtilityClasses.Tables;

public class BorderSpacingTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BorderSpacing()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "border-spacing-4",
                EscapedClassName = ".border-spacing-4",
                Styles =
                    """
                    --sf-border-spacing-x: calc(var(--spacing) * 4);
                    --sf-border-spacing-y: calc(var(--spacing) * 4);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-x-4",
                EscapedClassName = ".border-spacing-x-4",
                Styles =
                    """
                    --sf-border-spacing-x: calc(var(--spacing) * 4);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-y-4",
                EscapedClassName = ".border-spacing-y-4",
                Styles =
                    """
                    --sf-border-spacing-y: calc(var(--spacing) * 4);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-[1.25rem]",
                EscapedClassName = @".border-spacing-\[1\.25rem\]",
                Styles =
                    """
                    --sf-border-spacing-x: 1.25rem;
                    --sf-border-spacing-y: 1.25rem;
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-x-[1.25rem]",
                EscapedClassName = @".border-spacing-x-\[1\.25rem\]",
                Styles =
                    """
                    --sf-border-spacing-x: 1.25rem;
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-y-[1.25rem]",
                EscapedClassName = @".border-spacing-y-\[1\.25rem\]",
                Styles =
                    """
                    --sf-border-spacing-y: 1.25rem;
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-(--my-length)",
                EscapedClassName = @".border-spacing-\(--my-length\)",
                Styles =
                    """
                    --sf-border-spacing-x: var(--my-length);
                    --sf-border-spacing-y: var(--my-length);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-x-(--my-length)",
                EscapedClassName = @".border-spacing-x-\(--my-length\)",
                Styles =
                    """
                    --sf-border-spacing-x: var(--my-length);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "border-spacing-y-(--my-length)",
                EscapedClassName = @".border-spacing-y-\(--my-length\)",
                Styles =
                    """
                    --sf-border-spacing-y: var(--my-length);
                    border-spacing: var(--sf-border-spacing-x) var(--sf-border-spacing-y)
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
