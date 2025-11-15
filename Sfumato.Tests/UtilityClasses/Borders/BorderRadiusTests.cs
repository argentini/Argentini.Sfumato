namespace Sfumato.Tests.UtilityClasses.Borders;

public class BorderRadiusTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BorderRadius()
    {
        var testClasses = new List<TestClass>();

        foreach (var border in Entities.UtilityClasses.Borders.BorderRadius.Borders)
        {
            testClasses.AddRange(
                new()
                {
                    ClassName = $"{border.Key}-xl",
                    EscapedClassName = $".{border.Key}-xl",
                    Styles = border.Value.Replace("{0}", "var(--radius-xl)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-none",
                    EscapedClassName = $".{border.Key}-none",
                    Styles = border.Value.Replace("{0}", "0"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-full",
                    EscapedClassName = $".{border.Key}-full",
                    Styles = border.Value.Replace("{0}", "calc(infinity * 1px)"),
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
                    ClassName = $"{border.Key}-[var(--my-radius)]",
                    EscapedClassName = @$".{border.Key}-\[var\(--my-radius\)\]",
                    Styles = border.Value.Replace("{0}", "var(--my-radius)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-(--my-radius)",
                    EscapedClassName = @$".{border.Key}-\(--my-radius\)",
                    Styles = border.Value.Replace("{0}", "var(--my-radius)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-[length:var(--my-radius)]",
                    EscapedClassName = @$".{border.Key}-\[length\:var\(--my-radius\)\]",
                    Styles = border.Value.Replace("{0}", "var(--my-radius)"),
                    IsValid = true,
                    IsImportant = false,
                },
                new()
                {
                    ClassName = $"{border.Key}-(length:--my-radius)",
                    EscapedClassName = @$".{border.Key}-\(length\:--my-radius\)",
                    Styles = border.Value.Replace("{0}", "var(--my-radius)"),
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
