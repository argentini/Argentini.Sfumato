namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridAutoFlowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GridAutoFlow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "grid-flow-row",
                EscapedClassName = ".grid-flow-row",
                Styles =
                    """
                    grid-auto-flow: row;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-flow-col",
                EscapedClassName = ".grid-flow-col",
                Styles =
                    """
                    grid-auto-flow: column;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "grid-flow-row-dense",
                EscapedClassName = ".grid-flow-row-dense",
                Styles =
                    """
                    grid-auto-flow: row dense;
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
