namespace Argentini.Sfumato.Tests.UtilityClasses.Filters;

public class FilterTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Filter()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "filter-none",
                EscapedClassName = ".filter-none",
                Styles =
                    """
                    filter: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "filter-[url('filters.svg#filter-id')]",
                EscapedClassName = @".filter-\[url\(\'filters\.svg\#filter-id\'\)\]",
                Styles =
                    """
                    filter: url('filters.svg#filter-id');
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "filter-[blur(1rem)]",
                EscapedClassName = @".filter-\[blur\(1rem\)\]",
                Styles =
                    """
                    filter: blur(1rem);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "filter-(--my-filter)",
                EscapedClassName = @".filter-\(--my-filter\)",
                Styles =
                    """
                    filter: var(--my-filter);
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
