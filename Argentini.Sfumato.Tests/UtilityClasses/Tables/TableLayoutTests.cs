namespace Argentini.Sfumato.Tests.UtilityClasses.Tables;

public class TableLayoutTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TableLayout()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "table-auto",
                EscapedClassName = ".table-auto",
                Styles =
                    """
                    table-layout: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "table-fixed",
                EscapedClassName = ".table-fixed",
                Styles =
                    """
                    table-layout: fixed;
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
