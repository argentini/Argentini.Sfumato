namespace Sfumato.Tests.UtilityClasses.Typography;

public class TextDecorationLineTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextDecorationLine()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "underline",
                EscapedClassName = ".underline",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "no-underline",
                EscapedClassName = ".no-underline",
                Styles =
                    """
                    text-decoration-line: none;
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
