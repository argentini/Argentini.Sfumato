namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class OpacityTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Opacity()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "opacity-37",
                EscapedClassName = ".opacity-37",
                Styles =
                    """
                    opacity: calc(37 * 0.01);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-[0.67]",
                EscapedClassName = @".opacity-\[0\.67\]",
                Styles =
                    """
                    opacity: 0.67;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-[var(--my-opacity)]",
                EscapedClassName = @".opacity-\[var\(--my-opacity\)\]",
                Styles =
                    """
                    opacity: var(--my-opacity);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-(--my-opacity)",
                EscapedClassName = @".opacity-\(--my-opacity\)",
                Styles =
                    """
                    opacity: var(--my-opacity);
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
