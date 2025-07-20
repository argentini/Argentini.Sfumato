namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class OpacityTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Opacity()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "opacity-37",
                EscapedClassName = ".opacity-37",
                Styles =
                    """
                    opacity: calc(37 * 0.01);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-37.5",
                EscapedClassName = @".opacity-37\.5",
                Styles =
                    """
                    opacity: calc(37.5 * 0.01);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-[0.67]",
                EscapedClassName = @".opacity-\[0\.67\]",
                Styles =
                    """
                    opacity: 0.67;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-[var(--my-opacity)]",
                EscapedClassName = @".opacity-\[var\(--my-opacity\)\]",
                Styles =
                    """
                    opacity: var(--my-opacity);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "opacity-(--my-opacity)",
                EscapedClassName = @".opacity-\(--my-opacity\)",
                Styles =
                    """
                    opacity: var(--my-opacity);
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
