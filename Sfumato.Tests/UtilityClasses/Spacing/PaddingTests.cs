namespace Sfumato.Tests.UtilityClasses.Spacing;

public class PaddingTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Padding()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "p-px",
                EscapedClassName = ".p-px",
                Styles =
                    """
                    padding: 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-p-px",
                EscapedClassName = ".-p-px",
                Styles =
                    """
                    padding: -1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-3/4",
                EscapedClassName = @".p-3\/4",
                Styles =
                    """
                    padding: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-5",
                EscapedClassName = ".p-5",
                Styles =
                    """
                    padding: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-p-5",
                EscapedClassName = ".-p-5",
                Styles =
                    """
                    padding: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "px-10",
                EscapedClassName = ".px-10",
                Styles =
                    """
                    padding-inline: calc(var(--spacing) * 10);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-[1.25rem]",
                EscapedClassName = @".p-\[1\.25rem\]",
                Styles =
                    """
                    padding: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-(--my-padding)",
                EscapedClassName = @".p-\(--my-padding\)",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-p-(--my-padding)",
                EscapedClassName = @".-p-\(--my-padding\)",
                Styles =
                    """
                    padding: calc(var(--my-padding) * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-(length:--my-padding)",
                EscapedClassName = @".p-\(length\:--my-padding\)",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-[var(--my-padding)]",
                EscapedClassName = @".p-\[var\(--my-padding\)\]",
                Styles =
                    """
                    padding: var(--my-padding);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "p-[length:var(--my-padding)]",
                EscapedClassName = @".p-\[length\:var\(--my-padding\)\]",
                Styles =
                    """
                    padding: var(--my-padding);
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
