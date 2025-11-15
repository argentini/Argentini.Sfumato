namespace Sfumato.Tests.UtilityClasses.Interactivity;

public class AppearanceTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Appearance()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "appearance-none",
                EscapedClassName = ".appearance-none",
                Styles =
                    """
                    appearance: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "appearance-auto",
                EscapedClassName = ".appearance-auto",
                Styles =
                    """
                    appearance: auto;
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
