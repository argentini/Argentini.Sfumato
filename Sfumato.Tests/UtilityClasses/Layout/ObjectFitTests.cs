namespace Sfumato.Tests.UtilityClasses.Layout;

public class ObjectFitTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ObjectFit()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "object-contain",
                EscapedClassName = ".object-contain",
                Styles =
                    """
                    object-fit: contain;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "object-none",
                EscapedClassName = ".object-none",
                Styles =
                    """
                    object-fit: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "object-scale-down",
                EscapedClassName = ".object-scale-down",
                Styles =
                    """
                    object-fit: scale-down;
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
