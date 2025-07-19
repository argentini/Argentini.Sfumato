namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class ListStyleImageTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void ListStyleImage()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "list-image-[url(/images/header.jpg)]",
                EscapedClassName = @".list-image-\[url\(\/images\/header\.jpg\)\]",
                Styles =
                    """
                    list-style-image: url(/images/header.jpg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-image-[url(https://example.com/images/header.jpg)]",
                EscapedClassName = @".list-image-\[url\(https\:\/\/example\.com\/images\/header\.jpg\)\]",
                Styles =
                    """
                    list-style-image: url(https://example.com/images/header.jpg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-image-(url:--my-image)",
                EscapedClassName = @".list-image-\(url\:--my-image\)",
                Styles =
                    """
                    list-style-image: var(--my-image);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "list-image-[url:var(--my-image)]",
                EscapedClassName = @".list-image-\[url\:var\(--my-image\)\]",
                Styles =
                    """
                    list-style-image: var(--my-image);
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
