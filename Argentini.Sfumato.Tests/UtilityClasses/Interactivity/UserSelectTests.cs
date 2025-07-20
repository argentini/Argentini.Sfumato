namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class UserSelectTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void UserSelect()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "select-none",
                EscapedClassName = ".select-none",
                Styles =
                    """
                    user-select: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "select-text",
                EscapedClassName = ".select-text",
                Styles =
                    """
                    user-select: text;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "select-all",
                EscapedClassName = ".select-all",
                Styles =
                    """
                    user-select: all;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "select-auto",
                EscapedClassName = ".select-auto",
                Styles =
                    """
                    user-select: auto;
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
