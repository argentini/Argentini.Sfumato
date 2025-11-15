namespace Sfumato.Tests.UtilityClasses.Effects;

public class MaskClipTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskClip()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-clip-border",
                EscapedClassName = ".mask-clip-border",
                Styles =
                    """
                    -webkit-mask-clip: border-box;
                    mask-clip: border-box;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-no-clip",
                EscapedClassName = ".mask-no-clip",
                Styles =
                    """
                    -webkit-mask-clip: no-clip;
                    mask-clip: no-clip;
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
