namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class VerticalAlignTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void VerticalAlign()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "align-[4.5rem]",
                EscapedClassName = @".align-\[4\.5rem\]",
                Styles =
                    """
                    vertical-align: 4.5rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "align-(--my-offset)",
                EscapedClassName = @".align-\(--my-offset\)",
                Styles =
                    """
                    vertical-align: var(--my-offset);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "align-baseline",
                EscapedClassName = ".align-baseline",
                Styles =
                    """
                    vertical-align: baseline;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "align-super",
                EscapedClassName = ".align-super",
                Styles =
                    """
                    vertical-align: super;
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
