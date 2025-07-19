namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class ObjectPositionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ObjectPosition()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "object-top-left",
                EscapedClassName = ".object-top-left",
                Styles =
                    """
                    object-position: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "object-[25%_75%]",
                EscapedClassName = @".object-\[25\%_75\%\]",
                Styles =
                    """
                    object-position: 25% 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "object-[var(--my-pos)]",
                EscapedClassName = @".object-\[var\(--my-pos\)\]",
                Styles =
                    """
                    object-position: var(--my-pos);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "object-(--my-pos)",
                EscapedClassName = @".object-\(--my-pos\)",
                Styles =
                    """
                    object-position: var(--my-pos);
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
