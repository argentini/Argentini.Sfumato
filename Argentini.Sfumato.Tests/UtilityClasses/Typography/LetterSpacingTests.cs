namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class LetterSpacingTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void LetterSpacing()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "tracking-tighter",
                EscapedClassName = ".tracking-tighter",
                Styles =
                    """
                    letter-spacing: var(--tracking-tighter);
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--tracking-tighter" ],
            },
            new ()
            {
                ClassName = "tracking-[0.25rem]",
                EscapedClassName = @".tracking-\[0\.25rem\]",
                Styles =
                    """
                    letter-spacing: 0.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-tracking-[0.25rem]",
                EscapedClassName = @".-tracking-\[0\.25rem\]",
                Styles =
                    """
                    letter-spacing: calc(0.25rem * -1);
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
            
            testOutputHelper.WriteLine($"Typography / LetterSpacing => {test.ClassName}");
        }
    }
}
