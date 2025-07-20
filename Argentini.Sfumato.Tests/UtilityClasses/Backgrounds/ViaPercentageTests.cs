// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class ViaPercentageTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ViaPercentage()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "via-37%",
                EscapedClassName = @".via-37\%",
                Styles =
                    """
                    --sf-gradient-via-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-[37%]",
                EscapedClassName = @".via-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-via-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-(percentage:--my-pct)",
                EscapedClassName = @".via-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-via-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-[percentage:var(--my-pct)]",
                EscapedClassName = @".via-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-via-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            TestOutputHelper?.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
