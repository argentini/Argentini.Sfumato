namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class CursorTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Cursor()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "cursor-auto",
                EscapedClassName = ".cursor-auto",
                Styles =
                    """
                    cursor: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "cursor-pointer",
                EscapedClassName = ".cursor-pointer",
                Styles =
                    """
                    cursor: pointer;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "cursor-[grabbing]",
                EscapedClassName = @".cursor-\[grabbing\]",
                Styles =
                    """
                    cursor: grabbing;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "cursor-(--my-cursor)",
                EscapedClassName = @".cursor-\(--my-cursor\)",
                Styles =
                    """
                    cursor: var(--my-cursor);
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
