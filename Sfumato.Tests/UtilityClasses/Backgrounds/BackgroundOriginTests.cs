namespace Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundOriginTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundOrigin()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-origin-padding",
                EscapedClassName = ".bg-origin-padding",
                Styles =
                    """
                    background-origin: padding-box;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-origin-content",
                EscapedClassName = ".bg-origin-content",
                Styles =
                    """
                    background-origin: content-box;
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
