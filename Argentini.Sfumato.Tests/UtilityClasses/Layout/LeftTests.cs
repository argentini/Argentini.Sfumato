namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class LeftTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Left()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "left-auto",
                EscapedClassName = ".left-auto",
                Styles =
                    """
                    left: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "left-px",
                EscapedClassName = ".left-px",
                Styles =
                    """
                    left: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "left-3/4",
                EscapedClassName = @".left-3\/4",
                Styles =
                    """
                    left: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-left-3/4",
                EscapedClassName = @".-left-3\/4",
                Styles =
                    """
                    left: calc(75% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-5",
                EscapedClassName = ".left-5",
                Styles =
                    """
                    left: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-left-5",
                EscapedClassName = ".-left-5",
                Styles =
                    """
                    left: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-[1.25rem]",
                EscapedClassName = @".left-\[1\.25rem\]",
                Styles =
                    """
                    left: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "-left-[1.25rem]",
                EscapedClassName = @".-left-\[1\.25rem\]",
                Styles =
                    """
                    left: calc(1.25rem * -1);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-(--my-left)",
                EscapedClassName = @".left-\(--my-left\)",
                Styles =
                    """
                    left: var(--my-left);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-(length:--my-left)",
                EscapedClassName = @".left-\(length\:--my-left\)",
                Styles =
                    """
                    left: var(--my-left);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-[var(--my-left)]",
                EscapedClassName = @".left-\[var\(--my-left\)\]",
                Styles =
                    """
                    left: var(--my-left);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "left-[length:var(--my-left)]",
                EscapedClassName = @".left-\[length\:var\(--my-left\)\]",
                Styles =
                    """
                    left: var(--my-left);
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
