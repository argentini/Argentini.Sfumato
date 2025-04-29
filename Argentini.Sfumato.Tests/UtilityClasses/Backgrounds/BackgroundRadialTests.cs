namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundRadialTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BackgroundRadial()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-radial",
                EscapedClassName = ".bg-radial",
                Styles =
                    """
                    --sf-gradient-position: in oklab;
                    background-image: radial-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "bg-radial-[at_25%_25%]",
                EscapedClassName = @".bg-radial-\[at_25\%_25\%\]",
                Styles =
                    """
                    --sf-gradient-position: at 25% 25%;
                    background-image: radial-gradient(var(--sf-gradient-stops), at 25% 25%);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "bg-radial-(--my-radial)",
                EscapedClassName = @".bg-radial-\(--my-radial\)",
                Styles =
                    """
                    --sf-gradient-position: var(--my-radial);
                    background-image: radial-gradient(var(--sf-gradient-stops), var(--my-radial));
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
            },
            new ()
            {
                ClassName = "bg-radial-[var(--my-radial)]",
                EscapedClassName = @".bg-radial-\[var\(--my-radial\)\]",
                Styles =
                    """
                    --sf-gradient-position: var(--my-radial);
                    background-image: radial-gradient(var(--sf-gradient-stops), var(--my-radial));
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-gradient-position", "--sf-gradient-stops" ]
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
            
            testOutputHelper.WriteLine($"Backgrounds / BackgroundRadial => {test.ClassName}");
        }
    }
}
