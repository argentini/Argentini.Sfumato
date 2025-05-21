namespace Argentini.Sfumato.Tests.UtilityClasses.Filters;

public class DropShadowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void DropShadow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "drop-shadow-none",
                EscapedClassName = ".drop-shadow-none",
                Styles =
                    """
                    --sf-drop-shadow: ;
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-xs",
                EscapedClassName = ".drop-shadow-xs",
                Styles =
                    """
                    --sf-drop-shadow-alpha: 5%;
                    --sf-drop-shadow-color: oklch(0 0 0 / var(--sf-drop-shadow-alpha));
                    --sf-drop-shadow: 0 1px 1px var(--sf-drop-shadow-color);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-xs/37",
                EscapedClassName = @".drop-shadow-xs\/37",
                Styles =
                    """
                    --sf-drop-shadow-alpha: 37%;
                    --sf-drop-shadow-color: oklch(0 0 0 / var(--sf-drop-shadow-alpha));
                    --sf-drop-shadow: 0 1px 1px var(--sf-drop-shadow-color);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-2xl",
                EscapedClassName = ".drop-shadow-2xl",
                Styles =
                    """
                    --sf-drop-shadow-alpha: 15%;
                    --sf-drop-shadow-color: oklch(0 0 0 / var(--sf-drop-shadow-alpha));
                    --sf-drop-shadow: 0 25px 25px var(--sf-drop-shadow-color);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-[0_35px_35px_rgba(0,0,0,0.25)]",
                EscapedClassName = @".drop-shadow-\[0_35px_35px_rgba\(0\,0\,0\,0\.25\)\]",
                Styles =
                    """
                    --sf-drop-shadow-size: drop-shadow(0 35px 35px rgba(0,0,0,0.25));
                    --sf-drop-shadow: drop-shadow(var(--sf-drop-shadow-size));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-(--my-shadow)",
                EscapedClassName = @".drop-shadow-\(--my-shadow\)",
                Styles =
                    """
                    --sf-drop-shadow-size: drop-shadow(var(--my-shadow));
                    --sf-drop-shadow: drop-shadow(var(--sf-drop-shadow-size));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-lime-500",
                EscapedClassName = ".drop-shadow-lime-500",
                Styles =
                    """
                    --sf-drop-shadow-color: var(--color-lime-500);
                    --sf-drop-shadow: var(--sf-drop-shadow-size)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-lime-500/37",
                EscapedClassName = @".drop-shadow-lime-500\/37",
                Styles =
                    """
                    --sf-drop-shadow-color: color-mix(in oklab, var(--color-lime-500) 37%, transparent);
                    --sf-drop-shadow: var(--sf-drop-shadow-size)
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "drop-shadow-(color:--my-color)",
                EscapedClassName = @".drop-shadow-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-drop-shadow-color: var(--my-color);
                    --sf-drop-shadow: var(--sf-drop-shadow-size)
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
