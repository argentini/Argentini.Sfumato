namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class PlaceContentTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void PlaceContent()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "place-start",
                EscapedClassName = ".place-start",
                Styles =
                    """
                    place-content: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-end",
                EscapedClassName = ".place-end",
                Styles =
                    """
                    place-content: end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-end-safe",
                EscapedClassName = ".place-end-safe",
                Styles =
                    """
                    place-content: safe end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-center",
                EscapedClassName = ".place-center",
                Styles =
                    """
                    place-content: center;
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
