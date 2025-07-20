namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class RingTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Ring()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "ring",
                EscapedClassName = ".ring",
                Styles =
                    """
                    --sf-ring-shadow: var(--sf-ring-inset, ) 0 0 0 calc(1px + var(--sf-ring-offset-width)) var(--sf-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ring-4",
                EscapedClassName = ".ring-4",
                Styles =
                    """
                    --sf-ring-shadow: var(--sf-ring-inset, ) 0 0 0 calc(4px + var(--sf-ring-offset-width)) var(--sf-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ring-4.5",
                EscapedClassName = @".ring-4\.5",
                Styles =
                    """
                    --sf-ring-shadow: var(--sf-ring-inset, ) 0 0 0 calc(4.5px + var(--sf-ring-offset-width)) var(--sf-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "ring-[0_1px_#aabbcc]",
                EscapedClassName = @".ring-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    --sf-ring-shadow: 0 1px #aabbcc;
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
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
