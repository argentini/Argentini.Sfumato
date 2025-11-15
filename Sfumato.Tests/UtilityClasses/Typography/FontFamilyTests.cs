namespace Sfumato.Tests.UtilityClasses.Typography;

public class FontFamilyTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void FontFamily()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "font-sans",
                EscapedClassName = ".font-sans",
                Styles =
                    """
                    font-family: var(--font-sans);
                    font-feature-settings: var(--font-sans--font-feature-settings, normal);
                    font-variation-settings: var(--font-sans--font-variation-settings, normal);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "font-[ui-sans-serif,_system-ui]",
                EscapedClassName = @".font-\[ui-sans-serif\,_system-ui\]",
                Styles =
                    """
                    font-family: ui-sans-serif, system-ui;
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
