namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class TextShadowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TextShadow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "text-shadow-md",
                EscapedClassName = ".text-shadow-md",
                Styles =
                    """
                    text-shadow: var(--text-shadow-md);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--text-shadow-md", "--sf-text-shadow-color" ],
            },
            new ()
            {
                ClassName = "text-shadow-none",
                EscapedClassName = ".text-shadow-none",
                Styles =
                    """
                    text-shadow: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-shadow-[0_1px_#aabbcc]",
                EscapedClassName = @".text-shadow-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    text-shadow: 0 1px #aabbcc;
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
