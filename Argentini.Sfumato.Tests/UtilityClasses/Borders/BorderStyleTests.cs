namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class BorderStyleTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BorderStyle()
    {
        var appRunner = new AppRunner(new AppState());

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "border-solid",
                EscapedClassName = ".border-solid",
                Styles =
                    """
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
                    border-style: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.UsedCssCustomProperties.Length, cssClass.UsesCssCustomProperties.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.UsedCssCustomProperties.Length; i++)
            {
                Assert.Equal(test.UsedCssCustomProperties.ElementAt(i), cssClass.UsesCssCustomProperties.ElementAt(i));
            }
            
            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
