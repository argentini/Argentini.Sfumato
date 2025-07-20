namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexDirectionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void FlexDirection()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
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
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
