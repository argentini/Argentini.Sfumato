namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class OrderTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Order()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "order-first",
                EscapedClassName = ".order-first",
                Styles =
                    """
                    order: calc(-infinity);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-last",
                EscapedClassName = ".order-last",
                Styles =
                    """
                    order: calc(infinity);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-none",
                EscapedClassName = ".order-none",
                Styles =
                    """
                    order: 0;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-2",
                EscapedClassName = ".order-2",
                Styles =
                    """
                    order: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-[3]",
                EscapedClassName = @".order-\[3\]",
                Styles =
                    """
                    order: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-(--my-order)",
                EscapedClassName = @".order-\(--my-order\)",
                Styles =
                    """
                    order: var(--my-order);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-(integer:--my-order)",
                EscapedClassName = @".order-\(integer\:--my-order\)",
                Styles =
                    """
                    order: var(--my-order);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-[var(--my-order)]",
                EscapedClassName = @".order-\[var\(--my-order\)\]",
                Styles =
                    """
                    order: var(--my-order);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "order-[integer:var(--my-order)]",
                EscapedClassName = @".order-\[integer\:var\(--my-order\)\]",
                Styles =
                    """
                    order: var(--my-order);
                    """,
                IsValid = true,
                IsImportant = false,
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, selector: test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Styles, cssClass.Styles);

            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
