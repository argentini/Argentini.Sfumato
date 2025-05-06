namespace Argentini.Sfumato.Tests.UtilityClasses.TransitionsAnimations;

public class TransitionBehaviorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TransitionBehavior()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "transition-normal",
                EscapedClassName = ".transition-normal",
                Styles =
                    """
                    transition-behavior: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transition-discrete",
                EscapedClassName = ".transition-discrete",
                Styles =
                    """
                    transition-behavior: allow-discrete;
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
