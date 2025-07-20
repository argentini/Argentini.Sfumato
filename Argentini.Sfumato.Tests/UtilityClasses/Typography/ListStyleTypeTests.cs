namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class ListStyleTypeTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ListStyleType()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "list-disc",
                EscapedClassName = ".list-disc",
                Styles =
                    """
                    list-style-type: disc;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-none",
                EscapedClassName = ".list-none",
                Styles =
                    """
                    list-style-type: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-[upper-roman]",
                EscapedClassName = @".list-\[upper-roman\]",
                Styles =
                    """
                    list-style-type: upper-roman;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-(--my-bullet)",
                EscapedClassName = @".list-\(--my-bullet\)",
                Styles =
                    """
                    list-style-type: var(--my-bullet);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-[var(--my-bullet)]",
                EscapedClassName = @".list-\[var\(--my-bullet\)\]",
                Styles =
                    """
                    list-style-type: var(--my-bullet);
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
