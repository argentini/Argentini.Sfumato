namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskModeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskMode()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-alpha",
                EscapedClassName = ".mask-alpha",
                Styles =
                    """
                    -webkit-mask-source-type: alpha;
                    mask-mode: alpha;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-match",
                EscapedClassName = ".mask-match",
                Styles =
                    """
                    -webkit-mask-source-type: match-source;
                    mask-mode: match-source;
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
