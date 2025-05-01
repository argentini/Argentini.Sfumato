namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontStyleTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FontStyle()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "italic",
                EscapedClassName = ".italic",
                Styles =
                    """
                    font-style: italic;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "not-italic",
                EscapedClassName = ".not-italic",
                Styles =
                    """
                    font-style: normal;
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
