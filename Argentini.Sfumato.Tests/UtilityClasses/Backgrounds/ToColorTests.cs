// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ToColorTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ToColor()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "to-lime-800",
                EscapedClassName = ".to-lime-800",
                Styles =
                    """
                    --sf-gradient-to: var(--color-lime-800);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position))
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-(color:--my-color)",
                EscapedClassName = @".to-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-to: var(--my-color);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position))
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[#ff0000]",
                EscapedClassName = @".to-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-to: #ff0000;
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position))
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[var(--my-color)]",
                EscapedClassName = @".to-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-to: var(--my-color);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position))
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
