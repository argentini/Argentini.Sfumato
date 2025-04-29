// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ViaColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ViaColor()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "via-lime-800",
                EscapedClassName = ".via-lime-800",
                Styles =
                    """
                    --sf-gradient-via: var(--color-lime-800);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-lime-800", "--sf-gradient-via", "--sf-gradient-via-stops", "--sf-gradient-position", "--sf-gradient-from", "--sf-gradient-from-position", "--sf-gradient-via-position", "--sf-gradient-to", "--sf-gradient-to-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "via-(color:--my-color)",
                EscapedClassName = @".via-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-via: var(--my-color);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via", "--sf-gradient-via-stops", "--sf-gradient-position", "--sf-gradient-from", "--sf-gradient-from-position", "--sf-gradient-via-position", "--sf-gradient-to", "--sf-gradient-to-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "via-[#ff0000]",
                EscapedClassName = @".via-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-via: #ff0000;
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via", "--sf-gradient-via-stops", "--sf-gradient-position", "--sf-gradient-from", "--sf-gradient-from-position", "--sf-gradient-via-position", "--sf-gradient-to", "--sf-gradient-to-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "via-[var(--my-color)]",
                EscapedClassName = @".via-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-via: var(--my-color);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-via", "--sf-gradient-via-stops", "--sf-gradient-position", "--sf-gradient-from", "--sf-gradient-from-position", "--sf-gradient-via-position", "--sf-gradient-to", "--sf-gradient-to-position", "--sf-gradient-stops" ]
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
            
            testOutputHelper.WriteLine($"Backgrounds / ViaColor => {test.ClassName}");
        }
    }
}
