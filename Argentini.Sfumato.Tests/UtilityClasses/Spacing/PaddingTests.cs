namespace Argentini.Sfumato.Tests.UtilityClasses.Spacing;

public class PaddingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Padding()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "p-px",
                EscapedClassName = ".p-px",
                Styles =
                    """
                    padding: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-p-px",
                EscapedClassName = ".-p-px",
                Styles =
                    """
                    padding: -1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-3/4",
                EscapedClassName = @".p-3\/4",
                Styles =
                    """
                    padding: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-5",
                EscapedClassName = ".p-5",
                Styles =
                    """
                    padding: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-p-5",
                EscapedClassName = ".-p-5",
                Styles =
                    """
                    padding: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "px-10",
                EscapedClassName = ".px-10",
                Styles =
                    """
                    padding-inline: calc(var(--spacing) * 10);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-[1.25rem]",
                EscapedClassName = @".p-\[1\.25rem\]",
                Styles =
                    """
                    padding: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-(--my-padding)",
                EscapedClassName = @".p-\(--my-padding\)",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-p-(--my-padding)",
                EscapedClassName = @".-p-\(--my-padding\)",
                Styles =
                    """
                    padding: calc(var(--my-padding) * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-(length:--my-padding)",
                EscapedClassName = @".p-\(length\:--my-padding\)",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-[var(--my-padding)]",
                EscapedClassName = @".p-\[var\(--my-padding\)\]",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "p-[length:var(--my-padding)]",
                EscapedClassName = @".p-\[length\:var\(--my-padding\)\]",
                Styles =
                    """
                    padding: var(--my-padding);
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
