namespace Argentini.Sfumato.Tests.UtilityClasses.Typography;

public class FontSizeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void FontSize()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "text-base",
                EscapedClassName = ".text-base",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--text-base--line-height, initial);
                    letter-spacing: var(--text-base--letter-spacing, initial);
                    font-weight: var(--text-base--font-weight, initial);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-base/6",
                EscapedClassName = @".text-base\/6",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: calc(var(--spacing) * 6);
                    letter-spacing: var(--text-base--letter-spacing, initial);
                    font-weight: var(--text-base--font-weight, initial);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-base/[1.35rem]",
                EscapedClassName = @".text-base\/\[1\.35rem\]",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: 1.35rem;
                    letter-spacing: var(--text-base--letter-spacing, initial);
                    font-weight: var(--text-base--font-weight, initial);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-[1.25rem]",
                EscapedClassName = @".text-\[1\.25rem\]",
                Styles =
                    """
                    font-size: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-[1.25rem]/6",
                EscapedClassName = @".text-\[1\.25rem\]\/6",
                Styles =
                    """
                    font-size: 1.25rem;
                    line-height: calc(var(--spacing) * 6);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "text-[1.25rem]/[1.35rem]",
                EscapedClassName = @".text-\[1\.25rem\]\/\[1\.35rem\]",
                Styles =
                    """
                    font-size: 1.25rem;
                    line-height: 1.35rem;
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
