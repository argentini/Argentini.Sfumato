namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontStretchTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void FontStretch()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "font-stretch-ultra-condensed",
                EscapedClassName = ".font-stretch-ultra-condensed",
                Styles =
                    """
                    font-stretch: ultra-condensed;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-50%",
                EscapedClassName = @".font-stretch-50\%",
                Styles =
                    """
                    font-stretch: 50%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-[66.66%]",
                EscapedClassName = @".font-stretch-\[66\.66\%\]",
                Styles =
                    """
                    font-stretch: 66.66%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-[var(--my-pct)]",
                EscapedClassName = @".font-stretch-\[var\(--my-pct\)\]",
                Styles =
                    """
                    font-stretch: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-[percentage:var(--my-pct)]",
                EscapedClassName = @".font-stretch-\[percentage\:var\(--my-pct\)\]",
                Styles =
                    """
                    font-stretch: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-(--my-pct)",
                EscapedClassName = @".font-stretch-\(--my-pct\)",
                Styles =
                    """
                    font-stretch: var(--my-pct);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-stretch-(percentage:--my-pct)",
                EscapedClassName = @".font-stretch-\(percentage\:--my-pct\)",
                Styles =
                    """
                    font-stretch: var(--my-pct);
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
