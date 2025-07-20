namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class BorderWidthTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void BorderWidth()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>();

        foreach (var border in Entities.UtilityClasses.Borders.BorderWidth.BorderWidths)
        {
            testClasses.AddRange(
                new()
                {
                    ClassName = border.Key,
                    EscapedClassName = $".{border.Key}",
                    Styles = border.Value.Replace("{0}", "1px"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-0",
                    EscapedClassName = $".{border.Key}-0",
                    Styles = border.Value.Replace("{0}", "0px"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-4",
                    EscapedClassName = $".{border.Key}-4",
                    Styles = border.Value.Replace("{0}", "4px"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-4.5",
                    EscapedClassName = @$".{border.Key}-4\.5",
                    Styles = border.Value.Replace("{0}", "4.5px"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-[0.75rem]",
                    EscapedClassName = @$".{border.Key}-\[0\.75rem\]",
                    Styles = border.Value.Replace("{0}", "0.75rem"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-[length:var(--my-border)]",
                    EscapedClassName = @$".{border.Key}-\[length\:var\(--my-border\)\]",
                    Styles = border.Value.Replace("{0}", "var(--my-border)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-(length:--my-border)",
                    EscapedClassName = @$".{border.Key}-\(length\:--my-border\)",
                    Styles = border.Value.Replace("{0}", "var(--my-border)"),
                    IsValid = true,
                    IsImportant = false,
                }
            );
        }

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
