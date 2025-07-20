namespace Argentini.Sfumato.Tests.UtilityClasses.Tables;

public class TableLayoutTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TableLayout()
    {
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
