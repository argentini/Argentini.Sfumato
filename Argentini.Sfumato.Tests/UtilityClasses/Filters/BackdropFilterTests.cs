namespace Argentini.Sfumato.Tests.UtilityClasses.Filters;

public class BackdropFilterTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void BackdropFilter()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "backdrop-filter-none",
                EscapedClassName = ".backdrop-filter-none",
                Styles =
                    """
                    backdrop-filter: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "backdrop-filter-[url('filters.svg#filter-id')]",
                EscapedClassName = @".backdrop-filter-\[url\(\'filters\.svg\#filter-id\'\)\]",
                Styles =
                    """
                    backdrop-filter: url('filters.svg#filter-id');
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "backdrop-filter-[blur(1rem)]",
                EscapedClassName = @".backdrop-filter-\[blur\(1rem\)\]",
                Styles =
                    """
                    backdrop-filter: blur(1rem);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "backdrop-filter-(--my-filter)",
                EscapedClassName = @".backdrop-filter-\(--my-filter\)",
                Styles =
                    """
                    backdrop-filter: var(--my-filter);
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
