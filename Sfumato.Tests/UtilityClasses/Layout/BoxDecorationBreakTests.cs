namespace Sfumato.Tests.UtilityClasses.Layout;

public class BoxDecorationBreakTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BoxDecorationBreak()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "box-decoration-clone",
                EscapedClassName = ".box-decoration-clone",
                Styles =
                    """
                    box-decoration-break: clone;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "box-decoration-slice",
                EscapedClassName = ".box-decoration-slice",
                Styles =
                    """
                    box-decoration-break: slice;
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
