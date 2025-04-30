namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class AspectRatioTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void AspectRatio()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "aspect-square",
                EscapedClassName = ".aspect-square",
                Styles =
                    """
                    aspect-ratio: var(--aspect-square);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--aspect-square" ]
            },
            new ()
            {
                ClassName = "aspect-3/2",
                EscapedClassName = @".aspect-3\/2",
                Styles =
                    """
                    aspect-ratio: 3 / 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "aspect-[3_/_2]",
                EscapedClassName = @".aspect-\[3_\/_2\]",
                Styles =
                    """
                    aspect-ratio: 3 / 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "aspect-[var(--my-ratio)]",
                EscapedClassName = @".aspect-\[var\(--my-ratio\)\]",
                Styles =
                    """
                    aspect-ratio: var(--my-ratio);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "aspect-[ratio:var(--my-ratio)]",
                EscapedClassName = @".aspect-\[ratio\:var\(--my-ratio\)\]",
                Styles =
                    """
                    aspect-ratio: var(--my-ratio);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "aspect-(--my-ratio)",
                EscapedClassName = @".aspect-\(--my-ratio\)",
                Styles =
                    """
                    aspect-ratio: var(--my-ratio);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "aspect-(ratio:--my-ratio)",
                EscapedClassName = @".aspect-\(ratio\:--my-ratio\)",
                Styles =
                    """
                    aspect-ratio: var(--my-ratio);
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
            
            testOutputHelper.WriteLine($"Layout / AspectRatio => {test.ClassName}");
        }
    }
}
