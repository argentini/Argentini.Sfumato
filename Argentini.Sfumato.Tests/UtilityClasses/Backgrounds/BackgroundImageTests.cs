namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundImageTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundImage()
    {
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
                ClassName = "bg-[url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90)]",
                EscapedClassName = @".bg-\[url\(\/media\/ze0liffq\/alien-world\.jpg\?width\=1920\&quality\=90\)\]",
                Styles =
                    """
                    background-image: url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-[/media/ze0liffq/alien-world.jpg?width=1920&quality=90]",
                EscapedClassName = @".bg-\[\/media\/ze0liffq\/alien-world\.jpg\?width\=1920\&quality\=90\]",
                Styles =
                    """
                    background-image: url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90);
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
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
