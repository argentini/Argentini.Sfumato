namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class BorderStyleTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void BorderStyle()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "border-solid",
                EscapedClassName = ".border-solid",
                Styles =
                    """
                    --sf-border-style: solid;
                    border-style: solid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "border-hidden",
                EscapedClassName = ".border-hidden",
                Styles =
                    """
                    --sf-border-style: hidden;
                    border-style: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "border-none",
                EscapedClassName = ".border-none",
                Styles =
                    """
                    --sf-border-style: none;
                    border-style: none;
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
