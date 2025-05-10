namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskCompositeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void MaskComposite()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-add",
                EscapedClassName = ".mask-add",
                Styles =
                    """
                    mask-composite: add;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-exclude",
                EscapedClassName = ".mask-exclude",
                Styles =
                    """
                    mask-composite: exclude;
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
