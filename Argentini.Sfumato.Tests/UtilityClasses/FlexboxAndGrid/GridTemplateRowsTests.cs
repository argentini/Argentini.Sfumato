namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridTemplateRowsTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void GridTemplateRows()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "grid-rows-none",
                EscapedClassName = ".grid-rows-none",
                Styles =
                    """
                    grid-template-rows: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-rows-subgrid",
                EscapedClassName = ".grid-rows-subgrid",
                Styles =
                    """
                    grid-template-rows: subgrid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-rows-2",
                EscapedClassName = ".grid-rows-2",
                Styles =
                    """
                    grid-template-rows: repeat(2, minmax(0, 1fr));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-rows-[1fr_2fr]",
                EscapedClassName = @".grid-rows-\[1fr_2fr\]",
                Styles =
                    """
                    grid-template-rows: 1fr 2fr;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-rows-(--my-grid)",
                EscapedClassName = @".grid-rows-\(--my-grid\)",
                Styles =
                    """
                    grid-template-rows: var(--my-grid);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-rows-[var(--my-grid)]",
                EscapedClassName = @".grid-rows-\[var\(--my-grid\)\]",
                Styles =
                    """
                    grid-template-rows: var(--my-grid);
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
