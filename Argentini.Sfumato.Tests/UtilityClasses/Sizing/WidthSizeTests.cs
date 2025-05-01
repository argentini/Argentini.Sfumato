namespace Argentini.Sfumato.Tests.UtilityClasses.Sizing;

public class WidthSizeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void WidthSize()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            #region width
            
            new ()
            {
                ClassName = "w-auto",
                EscapedClassName = ".w-auto",
                Styles =
                    """
                    width: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-px",
                EscapedClassName = ".w-px",
                Styles =
                    """
                    width: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-fit",
                EscapedClassName = ".w-fit",
                Styles =
                    """
                    width: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "w-xl",
                EscapedClassName = ".w-xl",
                Styles =
                    """
                    width: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--container-xl" ],
            },
            new ()
            {
                ClassName = "w-3/4",
                EscapedClassName = @".w-3\/4",
                Styles =
                    """
                    width: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-5",
                EscapedClassName = ".w-5",
                Styles =
                    """
                    width: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-[1.25rem]",
                EscapedClassName = @".w-\[1\.25rem\]",
                Styles =
                    """
                    width: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-(--my-width)",
                EscapedClassName = @".w-\(--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-(length:--my-width)",
                EscapedClassName = @".w-\(length\:--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-[var(--my-width)]",
                EscapedClassName = @".w-\[var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "w-[length:var(--my-width)]",
                EscapedClassName = @".w-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            
            #endregion
            
            #region size
            
            new ()
            {
                ClassName = "size-auto",
                EscapedClassName = ".size-auto",
                Styles =
                    """
                    width: auto;
                    height: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-px",
                EscapedClassName = ".size-px",
                Styles =
                    """
                    width: 1px;
                    height: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-fit",
                EscapedClassName = ".size-fit",
                Styles =
                    """
                    width: fit-content;
                    height: fit-content;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "size-xl",
                EscapedClassName = ".size-xl",
                Styles =
                    """
                    width: var(--container-xl);
                    height: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--container-xl" ],
            },
            new ()
            {
                ClassName = "size-3/4",
                EscapedClassName = @".size-3\/4",
                Styles =
                    """
                    width: 75%;
                    height: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-5",
                EscapedClassName = ".size-5",
                Styles =
                    """
                    width: calc(var(--spacing) * 5);
                    height: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-[1.25rem]",
                EscapedClassName = @".size-\[1\.25rem\]",
                Styles =
                    """
                    width: 1.25rem;
                    height: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-(--my-width)",
                EscapedClassName = @".size-\(--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-(length:--my-width)",
                EscapedClassName = @".size-\(length\:--my-width\)",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-[var(--my-width)]",
                EscapedClassName = @".size-\[var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            new ()
            {
                ClassName = "size-[length:var(--my-width)]",
                EscapedClassName = @".size-\[length\:var\(--my-width\)\]",
                Styles =
                    """
                    width: var(--my-width);
                    height: var(--my-width);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing" ],
            },
            
            #endregion
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
