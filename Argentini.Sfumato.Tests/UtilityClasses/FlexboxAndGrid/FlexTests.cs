namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class FlexTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Flex()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "flex-auto",
                EscapedClassName = ".flex-auto",
                Styles =
                    """
                    flex: 1 1 auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-initial",
                EscapedClassName = ".flex-initial",
                Styles =
                    """
                    flex: 0 1 auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-none",
                EscapedClassName = ".flex-none",
                Styles =
                    """
                    flex: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-3/4",
                EscapedClassName = @".flex-3\/4",
                Styles =
                    """
                    flex: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-5",
                EscapedClassName = ".flex-5",
                Styles =
                    """
                    flex: 5;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-[2]",
                EscapedClassName = @".flex-\[2\]",
                Styles =
                    """
                    flex: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-[1_1_auto]",
                EscapedClassName = @".flex-\[1_1_auto\]",
                Styles =
                    """
                    flex: 1 1 auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-(--my-flex)",
                EscapedClassName = @".flex-\(--my-flex\)",
                Styles =
                    """
                    flex: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-(integer:--my-flex)",
                EscapedClassName = @".flex-\(integer\:--my-flex\)",
                Styles =
                    """
                    flex: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-[var(--my-flex)]",
                EscapedClassName = @".flex-\[var\(--my-flex\)\]",
                Styles =
                    """
                    flex: var(--my-flex);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "flex-[integer:var(--my-flex)]",
                EscapedClassName = @".flex-\[integer\:var\(--my-flex\)\]",
                Styles =
                    """
                    flex: var(--my-flex);
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
