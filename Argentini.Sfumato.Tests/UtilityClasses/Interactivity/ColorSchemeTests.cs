namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ColorSchemeTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ColorScheme()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scheme-normal",
                EscapedClassName = ".scheme-normal",
                Styles =
                    """
                    color-scheme: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scheme-only-light",
                EscapedClassName = ".scheme-only-light",
                Styles =
                    """
                    color-scheme: only light;
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
