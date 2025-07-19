namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class TextShadowColorTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void TextShadowColor()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "text-shadow-current",
                EscapedClassName = ".text-shadow-current",
                Styles = 
                    """
                    --sf-text-shadow-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-lime-500",
                EscapedClassName = ".text-shadow-lime-500",
                Styles = 
                    """
                    --sf-text-shadow-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-lime-500/25",
                EscapedClassName = @".text-shadow-lime-500\/25",
                Styles =
                    """
                    --sf-text-shadow-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-[#aabbcc]",
                EscapedClassName = @".text-shadow-\[\#aabbcc\]",
                Styles =
                    """
                    --sf-text-shadow-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-[#aabbcc]/25",
                EscapedClassName = @".text-shadow-\[\#aabbcc\]\/25",
                Styles =
                    """
                    --sf-text-shadow-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-[color:var(--my-color)]",
                EscapedClassName = @".text-shadow-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    --sf-text-shadow-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "text-shadow-(color:--my-color)",
                EscapedClassName = @".text-shadow-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-text-shadow-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            }
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
