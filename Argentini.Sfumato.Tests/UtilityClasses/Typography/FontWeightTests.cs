namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontWeightTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FontWeight()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "font-thin",
                EscapedClassName = ".font-thin",
                Styles =
                    """
                    --sf-font-weight: var(--font-weight-thin);
                    font-weight: var(--font-weight-thin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-bold",
                EscapedClassName = ".font-bold",
                Styles =
                    """
                    --sf-font-weight: var(--font-weight-bold);
                    font-weight: var(--font-weight-bold);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-light!",
                EscapedClassName = @".font-light\!",
                Styles =
                    """
                    --sf-font-weight: var(--font-weight-light) !important;
                    font-weight: var(--font-weight-light) !important;
                    """,
                IsValid = true,
                IsImportant = true,
            },
            new ()
            {
                ClassName = "font-[300]",
                EscapedClassName = @".font-\[300\]",
                Styles =
                    """
                    font-weight: 300;
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
