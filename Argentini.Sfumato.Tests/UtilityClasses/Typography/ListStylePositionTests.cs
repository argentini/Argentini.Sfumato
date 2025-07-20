namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class ListStylePositionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ListStylePosition()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "list-inside",
                EscapedClassName = ".list-inside",
                Styles =
                    """
                    list-style-position: inside;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-outside",
                EscapedClassName = ".list-outside",
                Styles =
                    """
                    list-style-position: outside;
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
