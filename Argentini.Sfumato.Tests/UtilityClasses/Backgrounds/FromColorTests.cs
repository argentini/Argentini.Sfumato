// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class FromColorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FromColor()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "from-lime-800",
                EscapedClassName = ".from-lime-800",
                Styles =
                    """
                    --sf-gradient-from: var(--color-lime-800);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-(color:--my-color)",
                EscapedClassName = @".from-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-from: var(--my-color);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-[#ff0000]",
                EscapedClassName = @".from-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-from: #ff0000;
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "from-[var(--my-color)]",
                EscapedClassName = @".from-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-from: var(--my-color);
                    --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
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
