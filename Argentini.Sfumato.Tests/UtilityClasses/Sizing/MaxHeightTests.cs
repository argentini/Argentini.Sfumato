namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class MaxHeightTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void MaxHeight()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "max-h-px",
                EscapedClassName = ".max-h-px",
                Styles =
                    """
                    max-height: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-fit",
                EscapedClassName = ".max-h-fit",
                Styles =
                    """
                    max-height: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-lh",
                EscapedClassName = ".max-h-lh",
                Styles =
                    """
                    max-height: 1lh;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-3/4",
                EscapedClassName = @".max-h-3\/4",
                Styles =
                    """
                    max-height: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-5",
                EscapedClassName = ".max-h-5",
                Styles =
                    """
                    max-height: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-[1.25rem]",
                EscapedClassName = @".max-h-\[1\.25rem\]",
                Styles =
                    """
                    max-height: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-(--my-height)",
                EscapedClassName = @".max-h-\(--my-height\)",
                Styles =
                    """
                    max-height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-(length:--my-height)",
                EscapedClassName = @".max-h-\(length\:--my-height\)",
                Styles =
                    """
                    max-height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-[var(--my-height)]",
                EscapedClassName = @".max-h-\[var\(--my-height\)\]",
                Styles =
                    """
                    max-height: var(--my-height);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "max-h-[length:var(--my-height)]",
                EscapedClassName = @".max-h-\[length\:var\(--my-height\)\]",
                Styles =
                    """
                    max-height: var(--my-height);
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
