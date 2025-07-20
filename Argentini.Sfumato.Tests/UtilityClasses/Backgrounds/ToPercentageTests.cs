// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ToPercentageTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ToPercentage()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "to-37%",
                EscapedClassName = @".to-37\%",
                Styles =
                    """
                    --sf-gradient-to-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[37%]",
                EscapedClassName = @".to-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-to-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-(percentage:--my-pct)",
                EscapedClassName = @".to-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-to-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[percentage:var(--my-pct)]",
                EscapedClassName = @".to-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-to-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
