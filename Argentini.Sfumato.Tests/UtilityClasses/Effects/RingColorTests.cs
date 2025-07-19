namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class RingColorTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void RingColor()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "ring-current",
                EscapedClassName = ".ring-current",
                Styles = 
                    """
                    --sf-ring-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-lime-500",
                EscapedClassName = ".ring-lime-500",
                Styles = 
                    """
                    --sf-ring-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-lime-500/25",
                EscapedClassName = @".ring-lime-500\/25",
                Styles =
                    """
                    --sf-ring-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-[#aabbcc]",
                EscapedClassName = @".ring-\[\#aabbcc\]",
                Styles =
                    """
                    --sf-ring-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-[#aabbcc]/25",
                EscapedClassName = @".ring-\[\#aabbcc\]\/25",
                Styles =
                    """
                    --sf-ring-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-[color:var(--my-color)]",
                EscapedClassName = @".ring-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    --sf-ring-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "ring-(color:--my-color)",
                EscapedClassName = @".ring-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-ring-color: var(--my-color);
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
