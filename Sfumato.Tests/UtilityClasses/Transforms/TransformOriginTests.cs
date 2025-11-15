namespace Sfumato.Tests.UtilityClasses.Transforms;

public class TransformOriginTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void TransformOrigin()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "origin-center",
                EscapedClassName = ".origin-center",
                Styles =
                    """
                    transform-origin: center;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "origin-top-left",
                EscapedClassName = ".origin-top-left",
                Styles =
                    """
                    transform-origin: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "origin-top",
                EscapedClassName = ".origin-top",
                Styles =
                    """
                    transform-origin: top;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "origin-[200%_150%]",
                EscapedClassName = @".origin-\[200\%_150\%\]",
                Styles =
                    """
                    transform-origin: 200% 150%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "origin-[var(--my-length)]",
                EscapedClassName = @".origin-\[var\(--my-length\)\]",
                Styles =
                    """
                    transform-origin: var(--my-length);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "origin-(--my-length)",
                EscapedClassName = @".origin-\(--my-length\)",
                Styles =
                    """
                    transform-origin: var(--my-length);
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
