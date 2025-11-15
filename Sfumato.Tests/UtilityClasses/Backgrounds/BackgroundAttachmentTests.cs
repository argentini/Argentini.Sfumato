namespace Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundAttachmentTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundAttachment()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-fixed",
                EscapedClassName = ".bg-fixed",
                Styles =
                    """
                    background-attachment: fixed;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-local",
                EscapedClassName = ".bg-local",
                Styles =
                    """
                    background-attachment: local;
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
