namespace Argentini.Sfumato.Tests.UtilityClasses.Backgrounds;

public class BackgroundConicTests(ITestOutputHelper testOutputHelper)
{
    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    [Fact]
    public void BackgroundConic()
    {
        var appRunner = new AppRunner(StringBuilderPool);
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "bg-conic",
                EscapedClassName = ".bg-conic",
                Styles =
                    """
                    --sf-gradient-position: in oklab;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic/srgb",
                EscapedClassName = @".bg-conic\/srgb",
                Styles =
                    """
                    --sf-gradient-position: in srgb;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic/srgb",
                EscapedClassName = @".bg-conic\/srgb",
                Styles =
                    """
                    --sf-gradient-position: in srgb;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic/decreasing",
                EscapedClassName = @".bg-conic\/decreasing",
                Styles =
                    """
                    --sf-gradient-position: in oklch decreasing hue;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic-180",
                EscapedClassName = ".bg-conic-180",
                Styles =
                    """
                    --sf-gradient-position: from 180deg in oklab;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-bg-conic-180",
                EscapedClassName = ".-bg-conic-180",
                Styles =
                    """
                    --sf-gradient-position: from -180deg in oklab;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic-[from_75deg_in_srgb]",
                EscapedClassName = @".bg-conic-\[from_75deg_in_srgb\]",
                Styles =
                    """
                    --sf-gradient-position: from 75deg in srgb;
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic-(--my-conic)",
                EscapedClassName = @".bg-conic-\(--my-conic\)",
                Styles =
                    """
                    --sf-gradient-position: var(--my-conic);
                    background-image: conic-gradient(var(--sf-gradient-stops));
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "bg-conic-[var(--my-conic)]",
                EscapedClassName = @".bg-conic-\[var\(--my-conic\)\]",
                Styles =
                    """
                    --sf-gradient-position: var(--my-conic);
                    background-image: conic-gradient(var(--sf-gradient-stops));
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
