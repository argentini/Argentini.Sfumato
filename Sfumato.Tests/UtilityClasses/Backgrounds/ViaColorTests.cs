// ReSharper disable RawStringCanBeSimplified

namespace Sfumato.Tests.UtilityClasses.Backgrounds;

public class ViaColorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ViaColor()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "via-lime-800",
                EscapedClassName = ".via-lime-800",
                Styles =
                    """
                    --sf-gradient-via: var(--color-lime-800);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-(color:--my-color)",
                EscapedClassName = @".via-\(color\:--my-color\)",
                Styles =
                    """
                    --sf-gradient-via: var(--my-color);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-[#ff0000]",
                EscapedClassName = @".via-\[\#ff0000\]",
                Styles =
                    """
                    --sf-gradient-via: #ff0000;
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "via-[var(--my-color)]",
                EscapedClassName = @".via-\[var\(--my-color\)\]",
                Styles =
                    """
                    --sf-gradient-via: var(--my-color);
                    --sf-gradient-via-stops: var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-via) var(--sf-gradient-via-position), var(--sf-gradient-to) var(--sf-gradient-to-position);
                    --sf-gradient-stops: var(--sf-gradient-via-stops);
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
