namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class OutlineStyleTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void OutlineStyle()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "outline-solid",
                EscapedClassName = ".outline-solid",
                Styles =
                    """
                    --sf-outline-style: solid;
                    outline-style: solid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-hidden",
                EscapedClassName = ".outline-hidden",
                Styles =
                    """
                    --sf-outline-style: hidden;
                    outline-style: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-none",
                EscapedClassName = ".outline-none",
                Styles =
                    """
                    --sf-outline-style: none;
                    outline-style: none;
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
