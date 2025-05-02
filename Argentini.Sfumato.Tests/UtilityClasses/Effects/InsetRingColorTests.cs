namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetRingColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetRingColor()
    {
        var appRunner = new AppRunner(new AppState());

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
                UsedCssCustomProperties = [ "--sf-inset-ring-color" ]
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
                UsedCssCustomProperties = [ "--color-lime-500", "--sf-inset-ring-color" ]
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
                UsedCssCustomProperties = [ "--color-lime-500", "--sf-inset-ring-color", ]
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
                UsedCssCustomProperties = [ "--sf-inset-ring-color" ]
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
                UsedCssCustomProperties = [ "--sf-inset-ring-color" ]
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
                UsedCssCustomProperties = [ "--sf-inset-ring-color" ]
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
                UsedCssCustomProperties = [ "--sf-inset-ring-color" ]
            }
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.UsedCssCustomProperties.Length, cssClass.UsesCssCustomProperties.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.UsedCssCustomProperties.Length; i++)
            {
                Assert.Equal(test.UsedCssCustomProperties.ElementAt(i), cssClass.UsesCssCustomProperties.ElementAt(i));
            }
            
            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
