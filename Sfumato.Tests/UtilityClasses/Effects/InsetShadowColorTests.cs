namespace Sfumato.Tests.UtilityClasses.Effects;

public class InsetShadowColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void InsetShadowColor()
    {
        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "inset-shadow-current",
                EscapedClassName = ".inset-shadow-current",
                Styles = 
                    """
                    --sf-inset-shadow-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-lime-500",
                EscapedClassName = ".inset-shadow-lime-500",
                Styles = 
                    """
                    --sf-inset-shadow-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-lime-500/25",
                EscapedClassName = @".inset-shadow-lime-500\/25",
                Styles =
                    """
                    --sf-inset-shadow-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-[#aabbcc]",
                EscapedClassName = @".inset-shadow-\[\#aabbcc\]",
                Styles =
                    """
                    --sf-inset-shadow-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-[#aabbcc]/25",
                EscapedClassName = @".inset-shadow-\[\#aabbcc\]\/25",
                Styles =
                    """
                    --sf-inset-shadow-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-[color:var(--my-color)]",
                EscapedClassName = @".inset-shadow-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    --sf-inset-shadow-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-shadow-(color:--my-color)",
                EscapedClassName = @".inset-shadow-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-inset-shadow-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            }
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
