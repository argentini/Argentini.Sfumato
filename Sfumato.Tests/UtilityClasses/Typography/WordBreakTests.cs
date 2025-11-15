namespace Sfumato.Tests.UtilityClasses.Typography;

public class WordBreakTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void WordBreak()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "break-normal",
                EscapedClassName = ".break-normal",
                Styles =
                    """
                    word-break: normal;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "break-keep",
                EscapedClassName = ".break-keep",
                Styles =
                    """
                    word-break: keep-all;
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
