namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridAutoColumnsTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void GridAutoColumns()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "auto-cols-auto",
                EscapedClassName = ".auto-cols-auto",
                Styles =
                    """
                    grid-auto-columns: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-cols-min",
                EscapedClassName = ".auto-cols-min",
                Styles =
                    """
                    grid-auto-columns: min-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-cols-fr",
                EscapedClassName = ".auto-cols-fr",
                Styles =
                    """
                    grid-auto-columns: minmax(0, 1fr);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-cols-[minmax(0,2fr)]",
                EscapedClassName = @".auto-cols-\[minmax\(0\,2fr\)\]",
                Styles =
                    """
                    grid-auto-columns: minmax(0,2fr);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-cols-(--my-cols)",
                EscapedClassName = @".auto-cols-\(--my-cols\)",
                Styles =
                    """
                    grid-auto-columns: var(--my-cols);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-cols-[var(--my-cols)]",
                EscapedClassName = @".auto-cols-\[var\(--my-cols\)\]",
                Styles =
                    """
                    grid-auto-columns: var(--my-cols);
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
