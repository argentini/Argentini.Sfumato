namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class BoxShadowColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BoxShadowColor()
    {
        var appRunner = new AppRunner(new AppState());

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "shadow-current",
                EscapedClassName = ".shadow-current",
                Styles = 
                    """
                    --sf-shadow-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-lime-500",
                EscapedClassName = ".shadow-lime-500",
                Styles = 
                    """
                    --sf-shadow-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-lime-500/25",
                EscapedClassName = @".shadow-lime-500\/25",
                Styles =
                    """
                    --sf-shadow-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-[#aabbcc]",
                EscapedClassName = @".shadow-\[\#aabbcc\]",
                Styles =
                    """
                    --sf-shadow-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-[#aabbcc]/25",
                EscapedClassName = @".shadow-\[\#aabbcc\]\/25",
                Styles =
                    """
                    --sf-shadow-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-[color:var(--my-color)]",
                EscapedClassName = @".shadow-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    --sf-shadow-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "shadow-(color:--my-color)",
                EscapedClassName = @".shadow-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-shadow-color: var(--my-color);
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
