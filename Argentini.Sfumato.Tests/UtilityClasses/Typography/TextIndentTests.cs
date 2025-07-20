namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class TextIndentTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextIndent()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "indent-4",
                EscapedClassName = ".indent-4",
                Styles =
                    """
                    text-indent: calc(var(--spacing) * 4);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "indent-4.5",
                EscapedClassName = @".indent-4\.5",
                Styles =
                    """
                    text-indent: calc(var(--spacing) * 4.5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-indent-4",
                EscapedClassName = ".-indent-4",
                Styles =
                    """
                    text-indent: calc(var(--spacing) * -4);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-indent-4.5",
                EscapedClassName = @".-indent-4\.5",
                Styles =
                    """
                    text-indent: calc(var(--spacing) * -4.5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "indent-[4.5rem]",
                EscapedClassName = @".indent-\[4\.5rem\]",
                Styles =
                    """
                    text-indent: 4.5rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "indent-(--my-offset)",
                EscapedClassName = @".indent-\(--my-offset\)",
                Styles =
                    """
                    text-indent: var(--my-offset);
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
