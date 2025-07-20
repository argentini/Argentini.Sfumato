namespace Argentini.Sfumato.Tests.UtilityClasses.Transforms;

public class PerspectiveTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Perspective()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "perspective-dramatic",
                EscapedClassName = ".perspective-dramatic",
                Styles =
                    """
                    perspective: var(--perspective-dramatic);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-distant",
                EscapedClassName = ".perspective-distant",
                Styles =
                    """
                    perspective: var(--perspective-distant);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-none",
                EscapedClassName = ".perspective-none",
                Styles =
                    """
                    perspective: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-[750px]",
                EscapedClassName = @".perspective-\[750px\]",
                Styles =
                    """
                    perspective: 750px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-[var(--my-length)]",
                EscapedClassName = @".perspective-\[var\(--my-length\)\]",
                Styles =
                    """
                    perspective: var(--my-length);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "perspective-(--my-length)",
                EscapedClassName = @".perspective-\(--my-length\)",
                Styles =
                    """
                    perspective: var(--my-length);
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
