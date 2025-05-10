namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class TextOverflowTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TextOverflow()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "truncate",
                EscapedClassName = ".truncate",
                Styles =
                    """
                    overflow: hidden;
                    text-overflow: ellipsis;
                    white-space: nowrap;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-clip",
                EscapedClassName = ".text-clip",
                Styles =
                    """
                    text-overflow: clip;
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
