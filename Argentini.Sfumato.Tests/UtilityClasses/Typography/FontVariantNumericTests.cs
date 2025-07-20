namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontVariantNumericTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void FontVariantNumeric()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "normal-nums",
                EscapedClassName = ".normal-nums",
                Styles =
                    """
                    --sf-ordinal: normal;
                    font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "diagonal-fractions",
                EscapedClassName = ".diagonal-fractions",
                Styles =
                    """
                    --sf-numeric-fraction: diagonal-fractions;
                    font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
