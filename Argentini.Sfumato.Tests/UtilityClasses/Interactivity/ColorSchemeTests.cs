namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ColorSchemeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ColorScheme()
    {
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
