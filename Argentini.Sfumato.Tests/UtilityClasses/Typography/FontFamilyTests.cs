namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontFamilyTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FontFamily()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "font-sans",
                EscapedClassName = ".font-sans",
                Styles =
                    """
                    font-family: var(--font-sans);
                    font-feature-settings: var(--font-sans--font-feature-settings, normal);
                    font-variation-settings: var(--font-sans--font-variation-settings, normal);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--font-sans", "--font-sans--font-feature-settings", "--font-sans--font-variation-settings" ],
            },
            new ()
            {
                ClassName = "font-[ui-sans-serif,_system-ui]",
                EscapedClassName = @".font-\[ui-sans-serif\,_system-ui\]",
                Styles =
                    """
                    font-family: ui-sans-serif, system-ui;
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
