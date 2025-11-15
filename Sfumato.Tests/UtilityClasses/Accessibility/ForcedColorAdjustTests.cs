namespace Sfumato.Tests.UtilityClasses.Accessibility;

public class ForcedColorAdjustTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ForcedColorAdjust()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "forced-color-adjust-auto",
                EscapedClassName = ".forced-color-adjust-auto",
                Styles =
                    """
                    forced-color-adjust: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "forced-color-adjust-none",
                EscapedClassName = ".forced-color-adjust-none",
                Styles =
                    """
                    forced-color-adjust: none;
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
