namespace Sfumato.Tests.UtilityClasses.Typography;

public class TextTransformTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextTransform()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "uppercase",
                EscapedClassName = ".uppercase",
                Styles =
                    """
                    text-transform: uppercase;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "normal-case",
                EscapedClassName = ".normal-case",
                Styles =
                    """
                    text-transform: none;
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
