namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class PlaceSelfTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void PlaceSelf()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "place-self-start",
                EscapedClassName = ".place-self-start",
                Styles =
                    """
                    place-self: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-self-end",
                EscapedClassName = ".place-self-end",
                Styles =
                    """
                    place-self: end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-self-center-safe",
                EscapedClassName = ".place-self-center-safe",
                Styles =
                    """
                    place-self: safe center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-self-stretch",
                EscapedClassName = ".place-self-stretch",
                Styles =
                    """
                    place-self: stretch;
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
