namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskOriginTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void MaskOrigin()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-origin-border",
                EscapedClassName = ".mask-origin-border",
                Styles =
                    """
                    mask-origin: border-box;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-origin-view",
                EscapedClassName = ".mask-origin-view",
                Styles =
                    """
                    mask-origin: view-box;
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
