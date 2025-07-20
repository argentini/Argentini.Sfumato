namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class WhiteSpaceTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void WhiteSpace()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "whitespace-normal",
                EscapedClassName = ".whitespace-normal",
                Styles =
                    """
                    white-space: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "whitespace-break-spaces",
                EscapedClassName = ".whitespace-break-spaces",
                Styles =
                    """
                    white-space: break-spaces;
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
