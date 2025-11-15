namespace Sfumato.Tests.UtilityClasses.Filters;

public class InvertTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Invert()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "invert",
                EscapedClassName = ".invert",
                Styles =
                    """
                    --sf-invert: invert(100%);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invert-37",
                EscapedClassName = ".invert-37",
                Styles =
                    """
                    --sf-invert: invert(37%);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invert-[.5]",
                EscapedClassName = @".invert-\[\.5\]",
                Styles =
                    """
                    --sf-invert: invert(.5);
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invert-(--my-gray)",
                EscapedClassName = @".invert-\(--my-gray\)",
                Styles =
                    """
                    --sf-invert: invert(var(--my-gray));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "invert-[var(--my-gray)]",
                EscapedClassName = @".invert-\[var\(--my-gray\)\]",
                Styles =
                    """
                    --sf-invert: invert(var(--my-gray));
                    filter: var(--sf-blur, ) var(--sf-brightness, ) var(--sf-contrast, ) var(--sf-grayscale, ) var(--sf-hue-rotate, ) var(--sf-invert, ) var(--sf-saturate, ) var(--sf-sepia, ) var(--sf-drop-shadow, );
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
