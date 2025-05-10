namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetShadowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetShadow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-shadow-sm",
                EscapedClassName = ".inset-shadow-sm",
                Styles =
                    """
                    box-shadow: var(--inset-shadow-sm);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-shadow-none",
                EscapedClassName = ".inset-shadow-none",
                Styles =
                    """
                    box-shadow: inset 0 0 #0000;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".inset-shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    box-shadow: inset 0 1px #aabbcc;
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
