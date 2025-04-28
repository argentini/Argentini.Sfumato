// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class FromColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FromColor()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "from-lime-800",
                EscapedClassName = ".from-lime-800",
                Styles =
                    """
                    --sf-gradient-from: var(--color-lime-800);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-lime-800", "--sf-gradient-from" ],
            },
            new ()
            {
                ClassName = "from-(color:--my-color)",
                EscapedClassName = @".from-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-from: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from" ],
            },
            new ()
            {
                ClassName = "from-[#ff0000]",
                EscapedClassName = @".from-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-from: #ff0000;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from" ],
            },
            new ()
            {
                ClassName = "from-[var(--my-color)]",
                EscapedClassName = @".from-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-from: var(--my-color);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-from" ],
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
            
            testOutputHelper.WriteLine($"Backgrounds / FromColor => {test.ClassName}");
        }
    }
}
