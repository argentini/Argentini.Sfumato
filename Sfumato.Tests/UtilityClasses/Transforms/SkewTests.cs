namespace Sfumato.Tests.UtilityClasses.Transforms;

public class SkewTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Skew()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "skew-3",
                EscapedClassName = ".skew-3",
                Styles =
                    """
                    --sf-skew-x: skewX(3deg);
                    --sf-skew-y: skewY(3deg);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-skew-3",
                EscapedClassName = ".-skew-3",
                Styles =
                    """
                    --sf-skew-x: skewX(calc(3deg * -1));
                    --sf-skew-y: skewY(calc(3deg * -1));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-[37.5rad]",
                EscapedClassName = @".skew-\[37\.5rad\]",
                Styles =
                    """
                    --sf-skew-x: skewX(37.5rad);
                    --sf-skew-y: skewY(37.5rad);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-[var(--my-skew)]",
                EscapedClassName = @".skew-\[var\(--my-skew\)\]",
                Styles =
                    """
                    --sf-skew-x: skewX(var(--my-skew));
                    --sf-skew-y: skewY(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-(--my-skew)",
                EscapedClassName = @".skew-\(--my-skew\)",
                Styles =
                    """
                    --sf-skew-x: skewX(var(--my-skew));
                    --sf-skew-y: skewY(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            
            
            new ()
            {
                ClassName = "skew-x-3",
                EscapedClassName = ".skew-x-3",
                Styles =
                    """
                    --sf-skew-x: skewX(3deg);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-skew-x-3",
                EscapedClassName = ".-skew-x-3",
                Styles =
                    """
                    --sf-skew-x: skewX(calc(3deg * -1));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-x-[37.5rad]",
                EscapedClassName = @".skew-x-\[37\.5rad\]",
                Styles =
                    """
                    --sf-skew-x: skewX(37.5rad);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-x-[var(--my-skew)]",
                EscapedClassName = @".skew-x-\[var\(--my-skew\)\]",
                Styles =
                    """
                    --sf-skew-x: skewX(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-x-(--my-skew)",
                EscapedClassName = @".skew-x-\(--my-skew\)",
                Styles =
                    """
                    --sf-skew-x: skewX(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            
            
            new ()
            {
                ClassName = "skew-y-3",
                EscapedClassName = ".skew-y-3",
                Styles =
                    """
                    --sf-skew-y: skewY(3deg);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-skew-y-3",
                EscapedClassName = ".-skew-y-3",
                Styles =
                    """
                    --sf-skew-y: skewY(calc(3deg * -1));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-y-[37.5rad]",
                EscapedClassName = @".skew-y-\[37\.5rad\]",
                Styles =
                    """
                    --sf-skew-y: skewY(37.5rad);
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-y-[var(--my-skew)]",
                EscapedClassName = @".skew-y-\[var\(--my-skew\)\]",
                Styles =
                    """
                    --sf-skew-y: skewY(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "skew-y-(--my-skew)",
                EscapedClassName = @".skew-y-\(--my-skew\)",
                Styles =
                    """
                    --sf-skew-y: skewY(var(--my-skew));
                    transform: var(--sf-rotate-x, ) var(--sf-rotate-y, ) var(--sf-rotate-z, ) var(--sf-skew-x, ) var(--sf-skew-y, );
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
