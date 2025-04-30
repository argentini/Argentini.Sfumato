namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class IsolationTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Isolation()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "isolate",
                EscapedClassName = ".isolate",
                Styles =
                    """
                    isolation: isolate;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "isolate-auto",
                EscapedClassName = ".isolate-auto",
                Styles =
                    """
                    isolation: auto;
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
