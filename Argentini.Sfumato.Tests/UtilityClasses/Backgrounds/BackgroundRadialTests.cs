namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundRadialTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundRadial()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-radial",
                EscapedClassName = ".bg-radial",
                Styles =
                    """
                    --sf-gradient-position: in oklab;
                    background-image: radial-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-radial-[at_25%_25%]",
                EscapedClassName = @".bg-radial-\[at_25\%_25\%\]",
                Styles =
                    """
                    --sf-gradient-position: at 25% 25%;
                    background-image: radial-gradient(var(--sf-gradient-stops, at 25% 25%));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-radial-(--my-radial)",
                EscapedClassName = @".bg-radial-\(--my-radial\)",
                Styles =
                    """
                    --sf-gradient-position: var(--my-radial);
                    background-image: radial-gradient(var(--sf-gradient-stops, var(--my-radial)));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-radial-[var(--my-radial)]",
                EscapedClassName = @".bg-radial-\[var\(--my-radial\)\]",
                Styles =
                    """
                    --sf-gradient-position: var(--my-radial);
                    background-image: radial-gradient(var(--sf-gradient-stops, var(--my-radial)));
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
