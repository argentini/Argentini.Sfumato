namespace Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexDirectionTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void FlexDirection()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "flex-row",
                EscapedClassName = ".flex-row",
                Styles =
                    """
                    flex-direction: row;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-col",
                EscapedClassName = ".flex-col",
                Styles =
                    """
                    flex-direction: column;
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
