namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class InsetYTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void InsetY()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "inset-y-auto",
                EscapedClassName = ".inset-y-auto",
                Styles =
                    """
                    inset-block: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-y-px",
                EscapedClassName = ".inset-y-px",
                Styles =
                    """
                    inset-block: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "inset-y-3/4",
                EscapedClassName = @".inset-y-3\/4",
                Styles =
                    """
                    inset-block: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-inset-y-3/4",
                EscapedClassName = @".-inset-y-3\/4",
                Styles =
                    """
                    inset-block: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-5",
                EscapedClassName = ".inset-y-5",
                Styles =
                    """
                    inset-block: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-inset-y-5",
                EscapedClassName = ".-inset-y-5",
                Styles =
                    """
                    inset-block: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-[1.25rem]",
                EscapedClassName = @".inset-y-\[1\.25rem\]",
                Styles =
                    """
                    inset-block: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-inset-y-[1.25rem]",
                EscapedClassName = @".-inset-y-\[1\.25rem\]",
                Styles =
                    """
                    inset-block: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-(--my-inset-y)",
                EscapedClassName = @".inset-y-\(--my-inset-y\)",
                Styles =
                    """
                    inset-block: var(--my-inset-y);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-(length:--my-inset-y)",
                EscapedClassName = @".inset-y-\(length\:--my-inset-y\)",
                Styles =
                    """
                    inset-block: var(--my-inset-y);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-[var(--my-inset-y)]",
                EscapedClassName = @".inset-y-\[var\(--my-inset-y\)\]",
                Styles =
                    """
                    inset-block: var(--my-inset-y);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "inset-y-[length:var(--my-inset-y)]",
                EscapedClassName = @".inset-y-\[length\:var\(--my-inset-y\)\]",
                Styles =
                    """
                    inset-block: var(--my-inset-y);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
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
