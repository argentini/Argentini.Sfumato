namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class StartTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Start()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "start-auto",
                EscapedClassName = ".start-auto",
                Styles =
                    """
                    inset-inline-start: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "start-px",
                EscapedClassName = ".start-px",
                Styles =
                    """
                    inset-inline-start: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "start-3/4",
                EscapedClassName = @".start-3\/4",
                Styles =
                    """
                    inset-inline-start: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-start-3/4",
                EscapedClassName = @".-start-3\/4",
                Styles =
                    """
                    inset-inline-start: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-5",
                EscapedClassName = ".start-5",
                Styles =
                    """
                    inset-inline-start: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-start-5",
                EscapedClassName = ".-start-5",
                Styles =
                    """
                    inset-inline-start: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-[1.25rem]",
                EscapedClassName = @".start-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline-start: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-start-[1.25rem]",
                EscapedClassName = @".-start-\[1\.25rem\]",
                Styles =
                    """
                    inset-inline-start: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-(--my-top)",
                EscapedClassName = @".start-\(--my-top\)",
                Styles =
                    """
                    inset-inline-start: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-(length:--my-top)",
                EscapedClassName = @".start-\(length\:--my-top\)",
                Styles =
                    """
                    inset-inline-start: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-[var(--my-top)]",
                EscapedClassName = @".start-\[var\(--my-top\)\]",
                Styles =
                    """
                    inset-inline-start: var(--my-top);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "start-[length:var(--my-top)]",
                EscapedClassName = @".start-\[length\:var\(--my-top\)\]",
                Styles =
                    """
                    inset-inline-start: var(--my-top);
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
