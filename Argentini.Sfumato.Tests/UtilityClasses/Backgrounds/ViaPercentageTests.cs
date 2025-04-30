// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ViaPercentageTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ViaPercentage()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "via-37%",
                EscapedClassName = @".via-37\%",
                Styles =
                    """
                    --sf-gradient-via-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via-position" ],
            },
            new ()
            {
                ClassName = "via-[37%]",
                EscapedClassName = @".via-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-via-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via-position" ],
            },
            new ()
            {
                ClassName = "via-(percentage:--my-pct)",
                EscapedClassName = @".via-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-via-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via-position" ],
            },
            new ()
            {
                ClassName = "via-[percentage:var(--my-pct)]",
                EscapedClassName = @".via-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-via-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via-position" ],
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
