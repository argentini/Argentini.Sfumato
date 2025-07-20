namespace Argentini.Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class TransitionPropertyTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TransitionProperty()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "transition",
                EscapedClassName = ".transition",
                Styles =
                    """
                    transition-property: color, background-color, border-color, outline-color, text-decoration-color, fill, stroke, --sf-gradient-from, --sf-gradient-via, --sf-gradient-to, opacity, box-shadow, transform, translate, scale, rotate, filter, -webkit-backdrop-filter, backdrop-filter;
                    transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                    transition-duration: var(--sf-duration, var(--default-transition-duration));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transition-none",
                EscapedClassName = ".transition-none",
                Styles =
                    """
                    transition-property: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transition-[height]",
                EscapedClassName = @".transition-\[height\]",
                Styles =
                    """
                    transition-property: height;
                    transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                    transition-duration: var(--sf-duration, var(--default-transition-duration));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transition-(--my-transition)",
                EscapedClassName = @".transition-\(--my-transition\)",
                Styles =
                    """
                    transition-property: var(--my-transition);
                    transition-timing-function: var(--sf-ease, var(--default-transition-timing-function));
                    transition-duration: var(--sf-duration, var(--default-transition-duration));
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
