namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontWeightTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FontWeight()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "font-thin",
                EscapedClassName = ".font-thin",
                Styles =
                    """
                    font-weight: var(--font-weight-thin);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--font-weight-thin" ],
            },
            new ()
            {
                ClassName = "font-bold",
                EscapedClassName = ".font-bold",
                Styles =
                    """
                    font-weight: var(--font-weight-bold);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--font-weight-bold" ],
            },
            new ()
            {
                ClassName = "font-light!",
                EscapedClassName = @".font-light\!",
                Styles =
                    """
                    font-weight: var(--font-weight-light) !important;
                    """,
                IsValid = true,
                IsImportant = true,
                UsedCssCustomProperties = [ "--font-weight-light" ],
            },
            new ()
            {
                ClassName = "font-[300]",
                EscapedClassName = @".font-\[300\]",
                Styles =
                    """
                    font-weight: 300;
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
            
            testOutputHelper.WriteLine($"Typography / FontWeight => {test.ClassName}");
        }
    }
}
