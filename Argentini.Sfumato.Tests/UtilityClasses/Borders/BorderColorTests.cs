namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class BorderColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BorderColor()
    {
        var appRunner = new AppRunner(new AppState());

        var testClasses = new List<TestClass>();

        foreach (var border in Entities.UtilityClasses.Borders.BorderColor.BorderColors)
        {
            testClasses.AddRange(
                new()
                {
                    ClassName = $"{border.Key}-current",
                    EscapedClassName = $".{border.Key}-current",
                    Styles = border.Value.Replace("{0}", "currentColor"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-lime-500",
                    EscapedClassName = $".{border.Key}-lime-500",
                    Styles = border.Value.Replace("{0}", "var(--color-lime-500)"),
                    IsValid = true,
                    IsImportant = false,
                    UsedCssCustomProperties = [ "--color-lime-500" ]
                },
                new()
                {
                    ClassName = $"{border.Key}-lime-500/25",
                    EscapedClassName = @$".{border.Key}-lime-500\/25",
                    Styles = border.Value.Replace("{0}", "color-mix(in oklab, var(--color-lime-500) 25%, transparent)"),
                    IsValid = true,
                    IsImportant = false,
                    UsedCssCustomProperties = [ "--color-lime-500" ]
                },
                new()
                {
                    ClassName = $"{border.Key}-[#aabbcc]",
                    EscapedClassName = @$".{border.Key}-\[\#aabbcc\]",
                    Styles = border.Value.Replace("{0}", "#aabbcc"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-[#aabbcc]/25",
                    EscapedClassName = @$".{border.Key}-\[\#aabbcc\]\/25",
                    Styles = border.Value.Replace("{0}", "rgba(170,187,204,0.25)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-[color:var(--my-color)]",
                    EscapedClassName = @$".{border.Key}-\[color\:var\(--my-color\)\]",
                    Styles = border.Value.Replace("{0}", "var(--my-color)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-(color:--my-color)",
                    EscapedClassName = @$".{border.Key}-\(color\:--my-color\)",
                    Styles = border.Value.Replace("{0}", "var(--my-color)"),
                    IsValid = true,
                    IsImportant = false,
                }
            );
        }

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
