namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class ClearTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Clear()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "clear-none",
                EscapedClassName = ".clear-none",
                Styles =
                    """
                    clear: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "clear-both",
                EscapedClassName = ".clear-both",
                Styles =
                    """
                    clear: both;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "clear-start",
                EscapedClassName = ".clear-start",
                Styles =
                    """
                    clear: inline-start;
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
