namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class BoxShadowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BoxShadow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "shadow-md",
                EscapedClassName = ".shadow-md",
                Styles =
                    """
                    box-shadow: var(--shadow-md);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shadow-none",
                EscapedClassName = ".shadow-none",
                Styles =
                    """
                    box-shadow: 0 0 #0000;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    box-shadow: 0 1px #aabbcc;
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
