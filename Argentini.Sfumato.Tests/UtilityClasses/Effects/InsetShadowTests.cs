namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetShadowTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void InsetShadow()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-shadow-sm",
                EscapedClassName = ".inset-shadow-sm",
                Styles =
                    """
                    --sf-inset-shadow-alpha: 5%;
                    --sf-inset-shadow-color: oklch(0 0 0 / var(--sf-inset-shadow-alpha));
                    --sf-inset-shadow: inset 0 2px 4px var(--sf-inset-shadow-color);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-shadow-sm/25",
                EscapedClassName = @".inset-shadow-sm\/25",
                Styles =
                    """
                    --sf-inset-shadow-alpha: 25%;
                    --sf-inset-shadow-color: oklch(0 0 0 / var(--sf-inset-shadow-alpha));
                    --sf-inset-shadow: inset 0 2px 4px var(--sf-inset-shadow-color);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-shadow-none",
                EscapedClassName = ".inset-shadow-none",
                Styles =
                    """
                    box-shadow: inset 0 0 #0000;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".inset-shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    box-shadow: inset 0 1px #aabbcc;
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
