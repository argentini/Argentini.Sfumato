namespace Sfumato.Tests.UtilityClasses.Typography;

public class HyphensTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Hyphens()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "hyphens-none",
                EscapedClassName = ".hyphens-none",
                Styles =
                    """
                    hyphens: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "hyphens-auto",
                EscapedClassName = ".hyphens-auto",
                Styles =
                    """
                    hyphens: auto;
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
