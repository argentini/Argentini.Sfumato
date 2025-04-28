// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class FromPercentageTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FromPercentage()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "from-37%",
                EscapedClassName = @".from-37\%",
                Styles =
                    """
                    --sf-gradient-from-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from-position" ],
            },
            new ()
            {
                ClassName = "from-[37%]",
                EscapedClassName = @".from-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-from-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from-position" ],
            },
            new ()
            {
                ClassName = "from-(percentage:--my-pct)",
                EscapedClassName = @".from-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-from-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from-position" ],
            },
            new ()
            {
                ClassName = "from-[percentage:var(--my-pct)]",
                EscapedClassName = @".from-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-from-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from-position" ],
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
            
            testOutputHelper.WriteLine($"Backgrounds / FromPercentage => {test.ClassName}");
        }
    }
}
