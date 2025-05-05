namespace Argentini.Sfumato.Tests.UtilityClasses.Filters;

public class ContrastTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Contrast()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "contrast-50",
                EscapedClassName = ".contrast-50",
                Styles =
                    """
                    --sf-contrast: contrast(50%);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "contrast-150",
                EscapedClassName = ".contrast-150",
                Styles =
                    """
                    --sf-contrast: contrast(150%);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "contrast-[.5]",
                EscapedClassName = @".contrast-\[\.5\]",
                Styles =
                    """
                    --sf-contrast: contrast(.5);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "contrast-(--my-filter)",
                EscapedClassName = @".contrast-\(--my-filter\)",
                Styles =
                    """
                    --sf-contrast: contrast(var(--my-filter));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
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
