namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundImageTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BackgroundImage()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-[url(/images/header.jpg)]",
                EscapedClassName = @".bg-\[url\(\/images\/header\.jpg\)\]",
                Styles =
                    """
                    background-image: url(/images/header.jpg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-[url(https://example.com/images/header.jpg)]",
                EscapedClassName = @".bg-\[url\(https\:\/\/example\.com\/images\/header\.jpg\)\]",
                Styles =
                    """
                    background-image: url(https://example.com/images/header.jpg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-(url:--my-image)",
                EscapedClassName = @".bg-\(url\:--my-image\)",
                Styles =
                    """
                    background-image: var(--my-image);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-[url:var(--my-image)]",
                EscapedClassName = @".bg-\[url\:var\(--my-image\)\]",
                Styles =
                    """
                    background-image: var(--my-image);
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
