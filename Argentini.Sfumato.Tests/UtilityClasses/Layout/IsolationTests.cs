namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class IsolationTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Isolation()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "isolate",
                EscapedClassName = ".isolate",
                Styles =
                    """
                    isolation: isolate;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "isolate-auto",
                EscapedClassName = ".isolate-auto",
                Styles =
                    """
                    isolation: auto;
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
