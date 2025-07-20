namespace Argentini.Sfumato.Tests.UtilityClasses.Transforms;

public class TransformTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Transform()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "transform",
                EscapedClassName = ".transform",
                Styles =
                    """
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transform-none",
                EscapedClassName = ".transform-none",
                Styles =
                    """
                    transform: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transform-cpu",
                EscapedClassName = ".transform-cpu",
                Styles =
                    """
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transform-gpu",
                EscapedClassName = ".transform-gpu",
                Styles =
                    """
                    transform: translateZ(0) var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "transform-[matrix(1,2,3,4,5,6)]",
                EscapedClassName = @".transform-\[matrix\(1\,2\,3\,4\,5\,6\)\]",
                Styles =
                    """
                    transform: matrix(1,2,3,4,5,6);
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
