namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class LineClampTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void LineClamp()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "line-clamp-none",
                EscapedClassName = ".line-clamp-none",
                Styles =
                    """
                    overflow: visible;
                    display: block;
                    -webkit-box-orient: horizontal;
                    -webkit-line-clamp: unset;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-3",
                EscapedClassName = @".line-clamp-3",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-[3]",
                EscapedClassName = @".line-clamp-\[3\]",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-[var(--my-lines)]",
                EscapedClassName = @".line-clamp-\[var\(--my-lines\)\]",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: var(--my-lines);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-[integer:var(--my-lines)]",
                EscapedClassName = @".line-clamp-\[integer\:var\(--my-lines\)\]",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: var(--my-lines);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-(--my-lines)",
                EscapedClassName = @".line-clamp-\(--my-lines\)",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: var(--my-lines);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "line-clamp-(integer:--my-lines)",
                EscapedClassName = @".line-clamp-\(integer\:--my-lines\)",
                Styles =
                    """
                    overflow: hidden;
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: var(--my-lines);
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
