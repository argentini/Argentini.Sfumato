namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class PositionTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Position()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "static",
                EscapedClassName = ".static",
                Styles =
                    """
                    position: static;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "fixed",
                EscapedClassName = ".fixed",
                Styles =
                    """
                    position: fixed;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "absolute",
                EscapedClassName = ".absolute",
                Styles =
                    """
                    position: absolute;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "relative",
                EscapedClassName = ".relative",
                Styles =
                    """
                    position: relative;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "sticky",
                EscapedClassName = ".sticky",
                Styles =
                    """
                    position: sticky;
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
