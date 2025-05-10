namespace Argentini.Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class AlignSelfTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void AlignSelf()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "self-start",
                EscapedClassName = ".self-start",
                Styles =
                    """
                    align-self: flex-start;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "self-end",
                EscapedClassName = ".self-end",
                Styles =
                    """
                    align-self: flex-end;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "self-baseline-last",
                EscapedClassName = ".self-baseline-last",
                Styles =
                    """
                    align-self: last baseline;
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
