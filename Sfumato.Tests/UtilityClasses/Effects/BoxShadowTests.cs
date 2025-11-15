namespace Sfumato.Tests.UtilityClasses.Effects;

public class BoxShadowTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BoxShadow()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "shadow-md",
                EscapedClassName = ".shadow-md",
                Styles =
                    $"""
                     --sf-shadow-alpha: 10%;
                     --sf-shadow-color: oklch(0 0 0 / var(--sf-shadow-alpha));
                     --sf-shadow: 0 4px 6px -1px var(--sf-shadow-color), 0 2px 4px -2px var(--sf-shadow-color);
                     box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                     """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shadow-md/25",
                EscapedClassName = @".shadow-md\/25",
                Styles =
                    """
                    --sf-shadow-alpha: 25%;
                    --sf-shadow-color: oklch(0 0 0 / var(--sf-shadow-alpha));
                    --sf-shadow: 0 4px 6px -1px var(--sf-shadow-color), 0 2px 4px -2px var(--sf-shadow-color);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shadow-none",
                EscapedClassName = ".shadow-none",
                Styles =
                    """
                    box-shadow: 0 0 #0000;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    box-shadow: 0 1px #aabbcc;
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
