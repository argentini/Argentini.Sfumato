namespace Sfumato.Tests.UtilityClasses.Transforms;

public class BackfaceVisibilityTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackfaceVisibility()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "backface-hidden",
                EscapedClassName = ".backface-hidden",
                Styles =
                    """
                    backface-visibility: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "backface-visible",
                EscapedClassName = ".backface-visible",
                Styles =
                    """
                    backface-visibility: visible;
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
