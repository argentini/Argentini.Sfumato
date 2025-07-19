namespace Argentini.Sfumato.Tests.UtilityClasses.Transforms;

public class RotateTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void Rotate()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "rotate-none",
                EscapedClassName = ".rotate-none",
                Styles =
                    """
                    rotate: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-0",
                EscapedClassName = ".rotate-0",
                Styles =
                    """
                    rotate: 0deg;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-[0]",
                EscapedClassName = @".rotate-\[0\]",
                Styles =
                    """
                    rotate: 0;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-37",
                EscapedClassName = ".rotate-37",
                Styles =
                    """
                    rotate: 37deg;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-rotate-37",
                EscapedClassName = ".-rotate-37",
                Styles =
                    """
                    rotate: calc(37deg * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-[37rad]",
                EscapedClassName = @".rotate-\[37rad\]",
                Styles =
                    """
                    rotate: 37rad;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-[var(--my-angle)]",
                EscapedClassName = @".rotate-\[var\(--my-angle\)\]",
                Styles =
                    """
                    rotate: var(--my-angle);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-(--my-angle)",
                EscapedClassName = @".rotate-\(--my-angle\)",
                Styles =
                    """
                    rotate: var(--my-angle);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-x-37",
                EscapedClassName = ".rotate-x-37",
                Styles =
                    """
                    --sf-rotate-x: 37deg;
                    transform: rotateX(37deg) var(--sf-rotate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-rotate-x-37",
                EscapedClassName = ".-rotate-x-37",
                Styles =
                    """
                    --sf-rotate-x: calc(37deg * -1);
                    transform: rotateX(calc(37deg * -1)) var(--sf-rotate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-y-37",
                EscapedClassName = ".rotate-y-37",
                Styles =
                    """
                    --sf-rotate-y: 37deg;
                    transform: var(--sf-rotate-x) rotateY(37deg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-rotate-y-37",
                EscapedClassName = ".-rotate-y-37",
                Styles =
                    """
                    --sf-rotate-y: calc(37deg * -1);
                    transform: var(--sf-rotate-x) rotateY(calc(37deg * -1));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "rotate-z-37",
                EscapedClassName = ".rotate-z-37",
                Styles =
                    """
                    --sf-rotate-z: 37deg;
                    transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ(37deg);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-rotate-z-37",
                EscapedClassName = ".-rotate-z-37",
                Styles =
                    """
                    --sf-rotate-z: calc(37deg * -1);
                    transform: var(--sf-rotate-x) var(--sf-rotate-y) rotateZ(calc(37deg * -1));
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
