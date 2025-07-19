namespace Argentini.Sfumato.Tests.UtilityClasses.Transforms;

public class ScaleTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Scale()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "scale-3d",
                EscapedClassName = ".scale-3d",
                Styles =
                    """
                    scale: var(--sf-scale-x) var(--sf-scale-y) var(--sf-scale-z);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-37",
                EscapedClassName = ".scale-37",
                Styles =
                    """
                    scale: 37% 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-scale-37",
                EscapedClassName = ".-scale-37",
                Styles =
                    """
                    scale: calc(37% * -1) calc(37% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-[37%]",
                EscapedClassName = @".scale-\[37\%\]",
                Styles =
                    """
                    scale: 37% 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-[var(--my-scale)]",
                EscapedClassName = @".scale-\[var\(--my-scale\)\]",
                Styles =
                    """
                    scale: var(--my-scale) var(--my-scale);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-(--my-scale)",
                EscapedClassName = @".scale-\(--my-scale\)",
                Styles =
                    """
                    scale: var(--my-scale) var(--my-scale);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-x-37",
                EscapedClassName = ".scale-x-37",
                Styles =
                    """
                    --sf-scale-x: 37%;
                    scale: 37% var(--sf-scale-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-scale-x-37",
                EscapedClassName = ".-scale-x-37",
                Styles =
                    """
                    --sf-scale-x: calc(37% * -1);
                    scale: calc(37% * -1) var(--sf-scale-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-y-37",
                EscapedClassName = ".scale-y-37",
                Styles =
                    """
                    --sf-scale-y: 37%;
                    scale: var(--sf-scale-x) 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-scale-y-37",
                EscapedClassName = ".-scale-y-37",
                Styles =
                    """
                    --sf-scale-y: calc(37% * -1);
                    scale: var(--sf-scale-x) calc(37% * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "scale-z-37",
                EscapedClassName = ".scale-z-37",
                Styles =
                    """
                    --sf-scale-z: 37%;
                    scale: var(--sf-scale-x) var(--sf-scale-y) 37%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-scale-z-37",
                EscapedClassName = ".-scale-z-37",
                Styles =
                    """
                    --sf-scale-z: calc(37% * -1);
                    scale: var(--sf-scale-x) var(--sf-scale-y) calc(37% * -1);
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
