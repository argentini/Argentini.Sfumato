namespace Argentini.Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class AnimationTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Animation()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "animate-none",
                EscapedClassName = ".animate-none",
                Styles =
                    """
                    animation: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "animate-spin",
                EscapedClassName = ".animate-spin",
                Styles =
                    """
                    animation: var(--animate-spin);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "animate-[wiggle_1s_ease-in-out_infinite]",
                EscapedClassName = @".animate-\[wiggle_1s_ease-in-out_infinite\]",
                Styles =
                    """
                    animation: wiggle 1s ease-in-out infinite;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "animate-(--my-animation)",
                EscapedClassName = @".animate-\(--my-animation\)",
                Styles =
                    """
                    animation: var(--my-animation);
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
