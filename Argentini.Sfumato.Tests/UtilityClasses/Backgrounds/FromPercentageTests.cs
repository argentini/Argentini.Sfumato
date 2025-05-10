// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class FromPercentageTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FromPercentage()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "from-37%",
                EscapedClassName = @".from-37\%",
                Styles =
                    """
                    --sf-gradient-from-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-[37%]",
                EscapedClassName = @".from-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-from-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-(percentage:--my-pct)",
                EscapedClassName = @".from-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-from-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-[percentage:var(--my-pct)]",
                EscapedClassName = @".from-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-from-position: var(--my-pct);
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
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
