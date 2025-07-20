namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MixBlendModeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MixBlendMode()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mix-blend-normal",
                EscapedClassName = ".mix-blend-normal",
                Styles =
                    """
                    mix-blend-mode: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mix-blend-difference",
                EscapedClassName = ".mix-blend-difference",
                Styles =
                    """
                    mix-blend-mode: difference;
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
