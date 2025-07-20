namespace Argentini.Sfumato.Tests.UtilityClasses.Tables;

public class CaptionSideTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void CaptionSide()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "caption-top",
                EscapedClassName = ".caption-top",
                Styles =
                    """
                    caption-side: top;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "caption-bottom",
                EscapedClassName = ".caption-bottom",
                Styles =
                    """
                    caption-side: bottom;
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
