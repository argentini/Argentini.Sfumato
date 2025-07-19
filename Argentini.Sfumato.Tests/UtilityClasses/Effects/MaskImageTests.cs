namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskImageTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void MaskImage()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-[url(/img/mask.png)]",
                EscapedClassName = @".mask-\[url\(\/img\/mask\.png\)\]",
                Styles =
                    """
                    -webkit-mask-image: url(/img/mask.png);
                    mask-image: url(/img/mask.png);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-(--my-mask)",
                EscapedClassName = @".mask-\(--my-mask\)",
                Styles =
                    """
                    -webkit-mask-image: var(--my-mask);
                    mask-image: var(--my-mask);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-(url:--my-mask)",
                EscapedClassName = @".mask-\(url\:--my-mask\)",
                Styles =
                    """
                    -webkit-mask-image: var(--my-mask);
                    mask-image: var(--my-mask);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-none",
                EscapedClassName = ".mask-none",
                Styles =
                    """
                    -webkit-mask-image: none;
                    mask-image: none;
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
