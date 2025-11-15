namespace Sfumato.Tests.UtilityClasses.Layout;

public class ContainerTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Container()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "@container",
                EscapedClassName = @".\@container",
                Styles =
                    """
                    container-type: inline-size;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "@container/george",
                EscapedClassName = @".\@container\/george",
                Styles =
                    """
                    container-type: inline-size;
                    container-name: george;
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
