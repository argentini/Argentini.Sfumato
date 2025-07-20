namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class TextUnderlineOffsetTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TextUnderlineOffset()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "underline-offset-4",
                EscapedClassName = ".underline-offset-4",
                Styles =
                    """
                    text-underline-offset: 4px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "underline-offset-4.5",
                EscapedClassName = @".underline-offset-4\.5",
                Styles =
                    """
                    text-underline-offset: 4.5px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-underline-offset-4",
                EscapedClassName = ".-underline-offset-4",
                Styles =
                    """
                    text-underline-offset: calc(4px * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-underline-offset-4.5",
                EscapedClassName = @".-underline-offset-4\.5",
                Styles =
                    """
                    text-underline-offset: calc(4.5px * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "underline-offset-[4.5rem]",
                EscapedClassName = @".underline-offset-\[4\.5rem\]",
                Styles =
                    """
                    text-underline-offset: 4.5rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "underline-offset-(--my-offset)",
                EscapedClassName = @".underline-offset-\(--my-offset\)",
                Styles =
                    """
                    text-underline-offset: var(--my-offset);
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
