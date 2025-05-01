namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class OutlineStyleTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void OutlineStyle()
    {
        var appRunner = new AppRunner(new AppState());

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "outline-solid",
                EscapedClassName = ".outline-solid",
                Styles =
                    """
                    outline-style: solid;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-hidden",
                EscapedClassName = ".outline-hidden",
                Styles =
                    """
                    outline-style: hidden;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "outline-none",
                EscapedClassName = ".outline-none",
                Styles =
                    """
                    outline-style: none;
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
