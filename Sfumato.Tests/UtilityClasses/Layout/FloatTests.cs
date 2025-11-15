namespace Sfumato.Tests.UtilityClasses.Layout;

public class FloatTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Float()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "float-none",
                EscapedClassName = ".float-none",
                Styles =
                    """
                    float: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "float-start",
                EscapedClassName = ".float-start",
                Styles =
                    """
                    float: inline-start;
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
