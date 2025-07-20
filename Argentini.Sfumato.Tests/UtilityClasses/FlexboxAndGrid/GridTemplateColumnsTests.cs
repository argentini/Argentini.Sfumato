namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridTemplateColumnsTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void GridTemplateColumns()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "grid-cols-none",
                EscapedClassName = ".grid-cols-none",
                Styles =
                    """
                    grid-template-columns: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-cols-subgrid",
                EscapedClassName = ".grid-cols-subgrid",
                Styles =
                    """
                    grid-template-columns: subgrid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-cols-2",
                EscapedClassName = ".grid-cols-2",
                Styles =
                    """
                    grid-template-columns: repeat(2, minmax(0, 1fr));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-cols-[1fr_2fr]",
                EscapedClassName = @".grid-cols-\[1fr_2fr\]",
                Styles =
                    """
                    grid-template-columns: 1fr 2fr;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-cols-(--my-grid)",
                EscapedClassName = @".grid-cols-\(--my-grid\)",
                Styles =
                    """
                    grid-template-columns: var(--my-grid);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-cols-[var(--my-grid)]",
                EscapedClassName = @".grid-cols-\[var\(--my-grid\)\]",
                Styles =
                    """
                    grid-template-columns: var(--my-grid);
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
