namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class PositionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Position()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "static",
                EscapedClassName = ".static",
                Styles =
                    """
                    position: static;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fixed",
                EscapedClassName = ".fixed",
                Styles =
                    """
                    position: fixed;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "absolute",
                EscapedClassName = ".absolute",
                Styles =
                    """
                    position: absolute;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "relative",
                EscapedClassName = ".relative",
                Styles =
                    """
                    position: relative;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "sticky",
                EscapedClassName = ".sticky",
                Styles =
                    """
                    position: sticky;
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
