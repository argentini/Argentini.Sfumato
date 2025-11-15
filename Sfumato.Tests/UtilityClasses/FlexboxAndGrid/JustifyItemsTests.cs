namespace Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class JustifyItemsTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void JustifyItems()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "justify-items-start",
                EscapedClassName = ".justify-items-start",
                Styles =
                    """
                    justify-items: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-items-end",
                EscapedClassName = ".justify-items-end",
                Styles =
                    """
                    justify-items: end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-items-end-safe",
                EscapedClassName = ".justify-items-end-safe",
                Styles =
                    """
                    justify-items: safe end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "justify-items-center",
                EscapedClassName = ".justify-items-center",
                Styles =
                    """
                    justify-items: center;
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
