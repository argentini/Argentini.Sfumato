namespace Sfumato.Tests.UtilityClasses.Borders;

public class BorderStyleTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BorderStyle()
    {
        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "border-solid",
                EscapedClassName = ".border-solid",
                Styles =
                    """
                    --sf-border-style: solid;
                    border-style: solid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "border-hidden",
                EscapedClassName = ".border-hidden",
                Styles =
                    """
                    --sf-border-style: hidden;
                    border-style: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "border-none",
                EscapedClassName = ".border-none",
                Styles =
                    """
                    --sf-border-style: none;
                    border-style: none;
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
