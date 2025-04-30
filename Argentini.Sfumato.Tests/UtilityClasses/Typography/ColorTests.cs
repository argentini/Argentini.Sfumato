namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class ColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Color()
    {
        var appRunner = new AppRunner(new AppState());
        
        appRunner.Library.ColorsByName.Add("fynydd-hex", "#0088ff");
        appRunner.Library.ColorsByName.Add("fynydd-rgb", "rgba(0, 136, 255, 1.0)");
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "text-lime-800",
                EscapedClassName = ".text-lime-800",
                Styles =
                    """
                    color: var(--color-lime-800);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-lime-800" ],
            },
            new ()
            {
                ClassName = "text-lime-800/37",
                EscapedClassName = @".text-lime-800\/37",
                Styles =
                    """
                    color: color-mix(in oklab, var(--color-lime-800) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-lime-800" ],
            },
            new ()
            {
                ClassName = "text-fynydd-hex/37",
                EscapedClassName = @".text-fynydd-hex\/37",
                Styles =
                    """
                    color: color-mix(in srgb, var(--color-fynydd-hex) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-fynydd-hex" ],
            },
            new ()
            {
                ClassName = "text-fynydd-rgb/37",
                EscapedClassName = @".text-fynydd-rgb\/37",
                Styles =
                    """
                    color: color-mix(in srgb, var(--color-fynydd-rgb) 37%, transparent);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--color-fynydd-rgb" ],
            },
            new ()
            {
                ClassName = "text-lime-800!",
                EscapedClassName = @".text-lime-800\!",
                Styles =
                    """
                    color: var(--color-lime-800) !important;
                    """,
                IsValid = true,
                IsImportant = true,
                UsedCssCustomProperties = [ "--color-lime-800" ],
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
