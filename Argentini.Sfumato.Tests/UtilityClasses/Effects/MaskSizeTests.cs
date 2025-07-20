namespace Argentini.Sfumato.Tests.UtilityClasses.Effects;

public class MaskSizeTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskSize()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-auto",
                EscapedClassName = ".mask-auto",
                Styles =
                    """
                    -webkit-mask-size: auto;
                    mask-size: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-contain",
                EscapedClassName = ".mask-contain",
                Styles =
                    """
                    -webkit-mask-size: contain;
                    mask-size: contain;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-size-[auto_100px]",
                EscapedClassName = @".mask-size-\[auto_100px\]",
                Styles =
                    """
                    -webkit-mask-size: auto 100px;
                    mask-size: auto 100px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-size-(--my-size)",
                EscapedClassName = @".mask-size-\(--my-size\)",
                Styles =
                    """
                    -webkit-mask-size: var(--my-size);
                    mask-size: var(--my-size);
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
