namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class OverflowTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Overflow()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "overflow-hidden",
                EscapedClassName = ".overflow-hidden",
                Styles =
                    """
                    overflow: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "overflow-auto",
                EscapedClassName = ".overflow-auto",
                Styles =
                    """
                    overflow: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "overflow-x-hidden",
                EscapedClassName = ".overflow-x-hidden",
                Styles =
                    """
                    overflow-x: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "overflow-y-hidden",
                EscapedClassName = ".overflow-y-hidden",
                Styles =
                    """
                    overflow-y: hidden;
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
