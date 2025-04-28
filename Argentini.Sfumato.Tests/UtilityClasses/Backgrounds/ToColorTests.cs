// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ToColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ToColor()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "to-lime-800",
                EscapedClassName = ".to-lime-800",
                Styles =
                    """
                    --sf-gradient-to: var(--color-lime-800);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-lime-800", "--sf-gradient-to" ],
            },
            new ()
            {
                ClassName = "to-(color:--my-color)",
                EscapedClassName = @".to-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-to: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-to" ],
            },
            new ()
            {
                ClassName = "to-[#ff0000]",
                EscapedClassName = @".to-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-to: #ff0000;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-to" ],
            },
            new ()
            {
                ClassName = "to-[var(--my-color)]",
                EscapedClassName = @".to-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-to: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-to" ],
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
            
            testOutputHelper.WriteLine($"Backgrounds / ToColor => {test.ClassName}");
        }
    }
}
