namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class VisibilityTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Visibility()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "visible",
                EscapedClassName = ".visible",
                Styles =
                    """
                    visibility: visible;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invisible",
                EscapedClassName = ".invisible",
                Styles =
                    """
                    visibility: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "collapse",
                EscapedClassName = ".collapse",
                Styles =
                    """
                    visibility: collapse;
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
