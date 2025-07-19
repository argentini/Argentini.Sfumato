namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class TextShadowTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void TextShadow()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "text-shadow-md",
                EscapedClassName = ".text-shadow-md",
                Styles =
                    """
                    --sf-text-shadow-alpha: 10%;
                    --sf-text-shadow-color: oklch(0 0 0 / var(--sf-text-shadow-alpha));
                    --sf-text-shadow: 0 1px 1px var(--sf-text-shadow-color), 0 1px 2px var(--sf-text-shadow-color), 0 2px 4px var(--sf-text-shadow-color);
                    text-shadow: var(--sf-text-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-shadow-md/25",
                EscapedClassName = @".text-shadow-md\/25",
                Styles =
                    """
                    --sf-text-shadow-alpha: 25%;
                    --sf-text-shadow-color: oklch(0 0 0 / var(--sf-text-shadow-alpha));
                    --sf-text-shadow: 0 1px 1px var(--sf-text-shadow-color), 0 1px 2px var(--sf-text-shadow-color), 0 2px 4px var(--sf-text-shadow-color);
                    text-shadow: var(--sf-text-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-shadow-none",
                EscapedClassName = ".text-shadow-none",
                Styles =
                    """
                    text-shadow: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".text-shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    text-shadow: 0 1px #aabbcc;
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
