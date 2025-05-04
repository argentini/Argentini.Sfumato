namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetRingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetRing()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-ring",
                EscapedClassName = ".inset-ring",
                Styles =
                    """
                    --sf-ring-inset: inset;
                    --sf-inset-ring-shadow: inset 0 0 0 1px var(--sf-inset-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-ring-4",
                EscapedClassName = ".inset-ring-4",
                Styles =
                    """
                    --sf-ring-inset: inset;
                    --sf-inset-ring-shadow: inset 0 0 0 4px var(--sf-inset-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-ring-[inset_0_1px_#aabbcc]",
                EscapedClassName = @".inset-ring-\[inset_0_1px_\#aabbcc\]",
                Styles =
                    """
                    --sf-ring-inset: inset;
                    --sf-inset-ring-shadow: inset 0 1px #aabbcc;
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
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
