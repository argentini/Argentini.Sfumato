namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridAutoRowsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GridAutoRows()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "auto-rows-auto",
                EscapedClassName = ".auto-rows-auto",
                Styles =
                    """
                    grid-auto-rows: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-rows-min",
                EscapedClassName = ".auto-rows-min",
                Styles =
                    """
                    grid-auto-rows: min-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-rows-fr",
                EscapedClassName = ".auto-rows-fr",
                Styles =
                    """
                    grid-auto-rows: minmax(0, 1fr);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-rows-[minmax(0,2fr)]",
                EscapedClassName = @".auto-rows-\[minmax\(0\,2fr\)\]",
                Styles =
                    """
                    grid-auto-rows: minmax(0,2fr);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-rows-(--my-rows)",
                EscapedClassName = @".auto-rows-\(--my-rows\)",
                Styles =
                    """
                    grid-auto-rows: var(--my-rows);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "auto-rows-[var(--my-rows)]",
                EscapedClassName = @".auto-rows-\[var\(--my-rows\)\]",
                Styles =
                    """
                    grid-auto-rows: var(--my-rows);
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
            Assert.Equal(test.UsedCssCustomProperties.Length, cssClass.UsesCssCustomProperties.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.UsedCssCustomProperties.Length; i++)
            {
                Assert.Equal(test.UsedCssCustomProperties.ElementAt(i), cssClass.UsesCssCustomProperties.ElementAt(i));
            }
            
            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
