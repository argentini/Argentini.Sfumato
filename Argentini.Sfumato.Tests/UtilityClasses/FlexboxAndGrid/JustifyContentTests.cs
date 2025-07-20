namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class JustifyContentTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void JustifyContent()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "justify-start",
                EscapedClassName = ".justify-start",
                Styles =
                    """
                    justify-content: flex-start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-end",
                EscapedClassName = ".justify-end",
                Styles =
                    """
                    justify-content: flex-end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-end-safe",
                EscapedClassName = ".justify-end-safe",
                Styles =
                    """
                    justify-content: safe flex-end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-center",
                EscapedClassName = ".justify-center",
                Styles =
                    """
                    justify-content: center;
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
