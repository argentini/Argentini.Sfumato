namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundSizeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void BackgroundSize()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-auto",
                EscapedClassName = ".bg-auto",
                Styles =
                    """
                    background-size: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-cover",
                EscapedClassName = ".bg-cover",
                Styles =
                    """
                    background-size: cover;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-size-[auto_10rem]",
                EscapedClassName = @".bg-size-\[auto_10rem\]",
                Styles =
                    """
                    background-size: auto 10rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-size-[var(--my-size)]",
                EscapedClassName = @".bg-size-\[var\(--my-size\)\]",
                Styles =
                    """
                    background-size: var(--my-size);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-size-(--my-size)",
                EscapedClassName = @".bg-size-\(--my-size\)",
                Styles =
                    """
                    background-size: var(--my-size);
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
