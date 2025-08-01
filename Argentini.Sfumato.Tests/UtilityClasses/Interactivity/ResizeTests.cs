namespace Argentini.Sfumato.Tests.UtilityClasses.Interactivity;

public class ResizeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Resize()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "resize-none",
                EscapedClassName = ".resize-none",
                Styles =
                    """
                    resize: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "resize",
                EscapedClassName = ".resize",
                Styles =
                    """
                    resize: both;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "resize-x",
                EscapedClassName = ".resize-x",
                Styles =
                    """
                    resize: horizontal;
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
