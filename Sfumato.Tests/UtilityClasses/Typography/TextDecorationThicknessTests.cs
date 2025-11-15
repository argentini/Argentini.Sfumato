namespace Sfumato.Tests.UtilityClasses.Typography;

public class TextDecorationThicknessTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextDecorationThickness()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "decoration-4",
                EscapedClassName = ".decoration-4",
                Styles =
                    """
                    text-decoration-thickness: 4px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-4.5",
                EscapedClassName = @".decoration-4\.5",
                Styles =
                    """
                    text-decoration-thickness: 4.5px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-[4.5rem]",
                EscapedClassName = @".decoration-\[4\.5rem\]",
                Styles =
                    """
                    text-decoration-thickness: 4.5rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-(--my-thickness)",
                EscapedClassName = @".decoration-\(--my-thickness\)",
                Styles =
                    """
                    text-decoration-thickness: var(--my-thickness);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-from-font",
                EscapedClassName = ".decoration-from-font",
                Styles =
                    """
                    text-decoration-thickness: from-font;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "decoration-auto",
                EscapedClassName = ".decoration-auto",
                Styles =
                    """
                    text-decoration-thickness: auto;
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
