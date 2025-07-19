namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class JustifySelfTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void JustifySelf()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "justify-self-start",
                EscapedClassName = ".justify-self-start",
                Styles =
                    """
                    justify-self: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-self-end",
                EscapedClassName = ".justify-self-end",
                Styles =
                    """
                    justify-self: end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-self-end-safe",
                EscapedClassName = ".justify-self-end-safe",
                Styles =
                    """
                    justify-self: safe end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-self-center",
                EscapedClassName = ".justify-self-center",
                Styles =
                    """
                    justify-self: center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-self-auto",
                EscapedClassName = ".justify-self-auto",
                Styles =
                    """
                    justify-self: auto;
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
