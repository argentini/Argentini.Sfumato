namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskOriginTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskOrigin()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-origin-border",
                EscapedClassName = ".mask-origin-border",
                Styles =
                    """
                    -webkit-mask-origin: border-box;
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
                    -webkit-mask-origin: view-box;
                    mask-origin: view-box;
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
