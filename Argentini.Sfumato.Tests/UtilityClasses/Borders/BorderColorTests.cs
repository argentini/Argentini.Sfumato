namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class BorderColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BorderColor()
    {
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
                },
                new()
                {
                    ClassName = $"{border.Key}-lime-500/25",
                    EscapedClassName = @$".{border.Key}-lime-500\/25",
                    Styles = border.Value.Replace("{0}", "color-mix(in oklab, var(--color-lime-500) 25%, transparent)"),
                    IsValid = true,
                    IsImportant = false,
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
