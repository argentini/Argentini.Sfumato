namespace Sfumato.Tests.UtilityClasses.Effects;

public class MaskPositionTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void MaskPosition()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "mask-top-left",
                EscapedClassName = ".mask-top-left",
                Styles =
                    """
                    -webkit-mask-position: top left;
                    mask-position: top left;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-bottom-right",
                EscapedClassName = ".mask-bottom-right",
                Styles =
                    """
                    -webkit-mask-position: bottom right;
                    mask-position: bottom right;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-position-[center_top_1rem]",
                EscapedClassName = @".mask-position-\[center_top_1rem\]",
                Styles =
                    """
                    -webkit-mask-position: center top 1rem;
                    mask-position: center top 1rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "mask-position-(--my-pos)",
                EscapedClassName = @".mask-position-\(--my-pos\)",
                Styles =
                    """
                    -webkit-mask-position: var(--my-pos);
                    mask-position: var(--my-pos);
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
