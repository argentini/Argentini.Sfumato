namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundPositionTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BackgroundPosition()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-top-left",
                EscapedClassName = ".bg-top-left",
                Styles =
                    """
                    background-position: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-[center_center]",
                EscapedClassName = @".bg-position-\[center_center\]",
                Styles =
                    """
                    background-position: center center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-[var(--my-pos)]",
                EscapedClassName = @".bg-position-\[var\(--my-pos\)\]",
                Styles =
                    """
                    background-position: var(--my-pos);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-(--my-pos)",
                EscapedClassName = @".bg-position-\(--my-pos\)",
                Styles =
                    """
                    background-position: var(--my-pos);
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
