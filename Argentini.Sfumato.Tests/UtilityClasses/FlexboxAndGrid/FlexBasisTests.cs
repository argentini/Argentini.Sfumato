namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexBasisTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void FlexBasis()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "basis-auto",
                EscapedClassName = ".basis-auto",
                Styles =
                    """
                    flex-basis: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-full",
                EscapedClassName = ".basis-full",
                Styles =
                    """
                    flex-basis: 100%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-xl",
                EscapedClassName = ".basis-xl",
                Styles =
                    """
                    flex-basis: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-3/4",
                EscapedClassName = @".basis-3\/4",
                Styles =
                    """
                    flex-basis: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-5",
                EscapedClassName = ".basis-5",
                Styles =
                    """
                    flex-basis: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-basis-5",
                EscapedClassName = ".-basis-5",
                Styles =
                    """
                    flex-basis: calc(var(--spacing) * -5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-[1.25rem]",
                EscapedClassName = @".basis-\[1\.25rem\]",
                Styles =
                    """
                    flex-basis: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-(--my-basis)",
                EscapedClassName = @".basis-\(--my-basis\)",
                Styles =
                    """
                    flex-basis: var(--my-basis);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-(length:--my-basis)",
                EscapedClassName = @".basis-\(length\:--my-basis\)",
                Styles =
                    """
                    flex-basis: var(--my-basis);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-[var(--my-basis)]",
                EscapedClassName = @".basis-\[var\(--my-basis\)\]",
                Styles =
                    """
                    flex-basis: var(--my-basis);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "basis-[length:var(--my-basis)]",
                EscapedClassName = @".basis-\[length\:var\(--my-basis\)\]",
                Styles =
                    """
                    flex-basis: var(--my-basis);
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
