namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class ContentTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Content()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "content-['']",
                EscapedClassName = @".content-\[\'\'\]",
                Styles =
                    """
                    content: '';
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "content-none",
                EscapedClassName = ".content-none",
                Styles =
                    """
                    content: none;
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
