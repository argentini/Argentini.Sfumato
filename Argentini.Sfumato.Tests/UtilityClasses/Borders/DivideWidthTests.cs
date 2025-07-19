namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class DivideWidthTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void DivideWidth()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>()
        {
            new()
            {
                ClassName = "divide-x",
                EscapedClassName = ".divide-x",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: 0px;
                        border-inline-end-width: 1px;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-y",
                EscapedClassName = ".divide-y",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-top-width: 0px;
                        border-bottom-width: 1px;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-0",
                EscapedClassName = ".divide-x-0",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: calc(0px * var(--sf-divide-x-reverse));
                        border-inline-end-width: calc(0px * calc(1 - var(--sf-divide-x-reverse)))
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-4",
                EscapedClassName = ".divide-x-4",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: calc(4px * var(--sf-divide-x-reverse));
                        border-inline-end-width: calc(4px * calc(1 - var(--sf-divide-x-reverse)))
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-4.5",
                EscapedClassName = @".divide-x-4\.5",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: calc(4.5px * var(--sf-divide-x-reverse));
                        border-inline-end-width: calc(4.5px * calc(1 - var(--sf-divide-x-reverse)))
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-[0.75rem]",
                EscapedClassName = @".divide-x-\[0\.75rem\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: 0px;
                        border-inline-end-width: 0.75rem;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-[length:var(--my-border)]",
                EscapedClassName = @".divide-x-\[length\:var\(--my-border\)\]",
                Styles = 
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: 0px;
                        border-inline-end-width: var(--my-border);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-x-(length:--my-border)",
                EscapedClassName = @".divide-x-\(length\:--my-border\)",
                Styles = 
                    """
                    & > :not(:last-child) {
                        border-inline-start-width: 0px;
                        border-inline-end-width: var(--my-border);
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            }
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
