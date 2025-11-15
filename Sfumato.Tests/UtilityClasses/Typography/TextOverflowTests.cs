namespace Sfumato.Tests.UtilityClasses.Typography;

public class TextOverflowTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextOverflow()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "truncate",
                EscapedClassName = ".truncate",
                Styles =
                    """
                    overflow: hidden;
                    text-overflow: ellipsis;
                    white-space: nowrap;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-clip",
                EscapedClassName = ".text-clip",
                Styles =
                    """
                    text-overflow: clip;
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
