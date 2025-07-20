namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetRingColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void InsetRingColor()
    {
        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "inset-ring-current",
                EscapedClassName = ".inset-ring-current",
                Styles = 
                    """
                    --sf-inset-ring-color: currentColor;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-lime-500",
                EscapedClassName = ".inset-ring-lime-500",
                Styles = 
                    """
                    --sf-inset-ring-color: var(--color-lime-500);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-lime-500/25",
                EscapedClassName = @".inset-ring-lime-500\/25",
                Styles =
                    """
                    --sf-inset-ring-color: color-mix(in oklab, var(--color-lime-500) 25%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-[#aabbcc]",
                EscapedClassName = @".inset-ring-\[\#aabbcc\]",
                Styles =
                    """
                    --sf-inset-ring-color: #aabbcc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-[#aabbcc]/25",
                EscapedClassName = @".inset-ring-\[\#aabbcc\]\/25",
                Styles =
                    """
                    --sf-inset-ring-color: rgba(170,187,204,0.25);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-[color:var(--my-color)]",
                EscapedClassName = @".inset-ring-\[color\:var\(--my-color\)\]",
                Styles =
                    """
                    --sf-inset-ring-color: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "inset-ring-(color:--my-color)",
                EscapedClassName = @".inset-ring-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-inset-ring-color: var(--my-color);
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
