namespace Argentini.Sfumato.Tests.UtilityClasses.Layout;

public class ColumnsTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Columns()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "columns-auto",
                EscapedClassName = ".columns-auto",
                Styles =
                    """
                    columns: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-4",
                EscapedClassName = ".columns-4",
                Styles =
                    """
                    columns: 4;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-xl",
                EscapedClassName = ".columns-xl",
                Styles =
                    """
                    columns: var(--container-xl);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-[var(--my-columns)]",
                EscapedClassName = @".columns-\[var\(--my-columns\)\]",
                Styles =
                    """
                    columns: var(--my-columns);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-[integer:var(--my-columns)]",
                EscapedClassName = @".columns-\[integer\:var\(--my-columns\)\]",
                Styles =
                    """
                    columns: var(--my-columns);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-(--my-columns)",
                EscapedClassName = @".columns-\(--my-columns\)",
                Styles =
                    """
                    columns: var(--my-columns);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "columns-(integer:--my-columns)",
                EscapedClassName = @".columns-\(integer\:--my-columns\)",
                Styles =
                    """
                    columns: var(--my-columns);
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
