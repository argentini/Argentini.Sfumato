namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class PlaceItemsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void PlaceItems()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "place-items-start",
                EscapedClassName = ".place-items-start",
                Styles =
                    """
                    place-items: start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-items-end",
                EscapedClassName = ".place-items-end",
                Styles =
                    """
                    place-items: end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "place-items-baseline",
                EscapedClassName = ".place-items-baseline",
                Styles =
                    """
                    place-items: baseline;
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
