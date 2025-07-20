namespace Argentini.Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class TransitionTimingFunctionTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void TransitionTimingFunction()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "ease-linear",
                EscapedClassName = ".ease-linear",
                Styles =
                    """
                    --sf-ease: linear;
                    transition-timing-function: linear;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ease-initial",
                EscapedClassName = ".ease-initial",
                Styles =
                    """
                    --sf-ease: initial;
                    transition-timing-function: initial;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ease-in",
                EscapedClassName = ".ease-in",
                Styles =
                    """
                    --sf-ease: var(--ease-in);
                    transition-timing-function: var(--ease-in);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ease-[150ms]",
                EscapedClassName = @".ease-\[150ms\]",
                Styles =
                    """
                    --sf-ease: 150ms;
                    transition-timing-function: 150ms;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ease-(--my-duration)",
                EscapedClassName = @".ease-\(--my-duration\)",
                Styles =
                    """
                    --sf-ease: var(--my-duration);
                    transition-timing-function: var(--my-duration);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ease-[cubic-bezier(0.95,0.05,0.795,0.035)]",
                EscapedClassName = @".ease-\[cubic-bezier\(0\.95\,0\.05\,0\.795\,0\.035\)\]",
                Styles =
                    """
                    --sf-ease: cubic-bezier(0.95,0.05,0.795,0.035);
                    transition-timing-function: cubic-bezier(0.95,0.05,0.795,0.035);
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
