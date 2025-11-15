namespace Sfumato.Tests.UtilityClasses.Effects;

public class MaskTypeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskType()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-type-alpha",
                EscapedClassName = ".mask-type-alpha",
                Styles =
                    """
                    mask-type: alpha;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-type-luminance",
                EscapedClassName = ".mask-type-luminance",
                Styles =
                    """
                    mask-type: luminance;
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
