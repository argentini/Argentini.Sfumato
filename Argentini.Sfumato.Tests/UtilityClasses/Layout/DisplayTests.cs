namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class DisplayTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Display()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inline",
                EscapedClassName = ".inline",
                Styles =
                    """
                    display: inline;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "block",
                EscapedClassName = ".block",
                Styles =
                    """
                    display: block;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "sr-only",
                EscapedClassName = ".sr-only",
                Styles =
                    """
                    position: absolute;
                    width: 1px;
                    height: 1px;
                    padding: 0;
                    margin: -1px;
                    overflow: hidden;
                    clip: rect(0, 0, 0, 0);
                    white-space: nowrap;
                    border-width: 0;
                    """,
                IsValid = true,
                IsImportant = false,
            },
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
