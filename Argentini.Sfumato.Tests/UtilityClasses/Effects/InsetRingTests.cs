namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class InsetRingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetRing()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-ring",
                EscapedClassName = ".inset-ring",
                Styles =
                    """
                    --sf-inset-ring-shadow: inset 0 0 0 1px var(--sf-inset-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-inset-ring-shadow", "--sf-inset-ring-color", "--sf-inset-shadow", "--sf-ring-offset-shadow", "--sf-ring-shadow", "--sf-shadow" ]
            },
            new ()
            {
                ClassName = "inset-ring-4",
                EscapedClassName = ".inset-ring-4",
                Styles =
                    """
                    --sf-inset-ring-shadow: inset 0 0 0 4px var(--sf-inset-ring-color, currentcolor);
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-inset-ring-shadow", "--sf-inset-ring-color", "--sf-inset-shadow", "--sf-ring-offset-shadow", "--sf-ring-shadow", "--sf-shadow" ]
            },
            new ()
            {
                ClassName = "inset-ring-[0_1px_#aabbcc]",
                EscapedClassName = @".inset-ring-\[0_1px_\#aabbcc\]",
                Styles =
                    """
                    --sf-inset-ring-shadow: inset 0 1px #aabbcc;
                    box-shadow: var(--sf-inset-shadow), var(--sf-inset-ring-shadow), var(--sf-ring-offset-shadow), var(--sf-ring-shadow), var(--sf-shadow);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-inset-ring-shadow", "--sf-inset-ring-color", "--sf-inset-shadow", "--sf-ring-offset-shadow", "--sf-ring-shadow", "--sf-shadow" ]
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
