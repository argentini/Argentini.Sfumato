namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class TopTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Top()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "top-auto",
                EscapedClassName = ".top-auto",
                Styles =
                    """
                    top: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "top-px",
                EscapedClassName = ".top-px",
                Styles =
                    """
                    top: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "top-3/4",
                EscapedClassName = @".top-3\/4",
                Styles =
                    """
                    top: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-top-3/4",
                EscapedClassName = @".-top-3\/4",
                Styles =
                    """
                    top: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-5",
                EscapedClassName = ".top-5",
                Styles =
                    """
                    top: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-top-5",
                EscapedClassName = ".-top-5",
                Styles =
                    """
                    top: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-[1.25rem]",
                EscapedClassName = @".top-\[1\.25rem\]",
                Styles =
                    """
                    top: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-top-[1.25rem]",
                EscapedClassName = @".-top-\[1\.25rem\]",
                Styles =
                    """
                    top: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-(--my-top)",
                EscapedClassName = @".top-\(--my-top\)",
                Styles =
                    """
                    top: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-(length:--my-top)",
                EscapedClassName = @".top-\(length\:--my-top\)",
                Styles =
                    """
                    top: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-[var(--my-top)]",
                EscapedClassName = @".top-\[var\(--my-top\)\]",
                Styles =
                    """
                    top: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "top-[length:var(--my-top)]",
                EscapedClassName = @".top-\[length\:var\(--my-top\)\]",
                Styles =
                    """
                    top: var(--my-top);
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
