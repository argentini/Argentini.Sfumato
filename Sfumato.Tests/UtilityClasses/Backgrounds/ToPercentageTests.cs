// ReSharper disable RawStringCanBeSimplified

namespace Sfumato.Tests.UtilityClasses.Backgrounds;

public class ToPercentageTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ToPercentage()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "to-37%",
                EscapedClassName = @".to-37\%",
                Styles =
                    """
                    --sf-gradient-to-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[37%]",
                EscapedClassName = @".to-\[37\%\]",
                Styles =
                    """
                    --sf-gradient-to-position: 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-(percentage:--my-pct)",
                EscapedClassName = @".to-\(percentage\:--my-pct\)",
                Styles =
                    """
                    --sf-gradient-to-position: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "to-[percentage:var(--my-pct)]",
                EscapedClassName = @".to-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    --sf-gradient-to-position: var(--my-pct);
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
