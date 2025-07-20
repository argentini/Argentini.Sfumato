namespace Argentini.Sfumato.Tests.UtilityClasses.Borders;

public class DivideStyleTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void DivideStyle()
    {
        var appRunner = new AppRunner(StringBuilderPool);

        var testClasses = new List<TestClass>
        {
            new()
            {
                ClassName = "divide-solid",
                EscapedClassName = ".divide-solid",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-style: solid;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-hidden",
                EscapedClassName = ".divide-hidden",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-style: hidden;
                    }
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new()
            {
                ClassName = "divide-none",
                EscapedClassName = ".divide-none",
                Styles =
                    """
                    & > :not(:last-child) {
                        border-style: none;
                    }
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
