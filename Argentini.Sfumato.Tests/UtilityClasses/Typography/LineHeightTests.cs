namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class LineHeightTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void LineHeight()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "leading-tight",
                EscapedClassName = ".leading-tight",
                Styles =
                    """
                    --sf-leading: var(--leading-tight);
                    line-height: var(--sf-leading);
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
                    --sf-leading: 1;
                    line-height: var(--sf-leading);
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
                    --sf-leading: 1.35rem;
                    line-height: var(--sf-leading);
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
                    --sf-leading: calc(1.35rem * -1);
                    line-height: var(--sf-leading);
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
                    --sf-leading: var(--my-line-height);
                    line-height: var(--sf-leading);
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
                    --sf-leading: var(--my-line-height);
                    line-height: var(--sf-leading);
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
                    --sf-leading: var(--my-line-height);
                    line-height: var(--sf-leading);
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
                    --sf-leading: var(--my-line-height);
                    line-height: var(--sf-leading);
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
