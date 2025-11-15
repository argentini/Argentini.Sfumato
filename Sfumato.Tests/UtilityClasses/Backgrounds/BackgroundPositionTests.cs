namespace Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundPositionTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void BackgroundPosition()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-top-left",
                EscapedClassName = ".bg-top-left",
                Styles =
                    """
                    background-position: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-[center_center]",
                EscapedClassName = @".bg-position-\[center_center\]",
                Styles =
                    """
                    background-position: center center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-[var(--my-pos)]",
                EscapedClassName = @".bg-position-\[var\(--my-pos\)\]",
                Styles =
                    """
                    background-position: var(--my-pos);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-position-(--my-pos)",
                EscapedClassName = @".bg-position-\(--my-pos\)",
                Styles =
                    """
                    background-position: var(--my-pos);
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
