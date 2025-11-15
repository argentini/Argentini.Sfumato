namespace Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridRowTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void GridRow()
    {
        var testClasses = new List<TestClass>()
        {
            #region row-span
            
            new ()
            {
                ClassName = "row-span-full",
                EscapedClassName = ".row-span-full",
                Styles =
                    """
                    grid-row: 1 / -1;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-span-2",
                EscapedClassName = ".row-span-2",
                Styles =
                    """
                    grid-row: span 2 / span 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-span-[3]",
                EscapedClassName = @".row-span-\[3\]",
                Styles =
                    """
                    grid-row: span 3 / span 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-span-(--my-span)",
                EscapedClassName = @".row-span-\(--my-span\)",
                Styles =
                    """
                    grid-row: span var(--my-span) / span var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-span-[var(--my-span)]",
                EscapedClassName = @".row-span-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-row: span var(--my-span) / span var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region row-start
            
            new ()
            {
                ClassName = "row-start-auto",
                EscapedClassName = ".row-start-auto",
                Styles =
                    """
                    grid-row-start: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-start-2",
                EscapedClassName = ".row-start-2",
                Styles =
                    """
                    grid-row-start: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-row-start-2",
                EscapedClassName = ".-row-start-2",
                Styles =
                    """
                    grid-row-start: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-start-[3]",
                EscapedClassName = @".row-start-\[3\]",
                Styles =
                    """
                    grid-row-start: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-start-(--my-span)",
                EscapedClassName = @".row-start-\(--my-span\)",
                Styles =
                    """
                    grid-row-start: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-start-[var(--my-span)]",
                EscapedClassName = @".row-start-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-row-start: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion

            #region row-end
            
            new ()
            {
                ClassName = "row-end-auto",
                EscapedClassName = ".row-end-auto",
                Styles =
                    """
                    grid-row-end: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-end-2",
                EscapedClassName = ".row-end-2",
                Styles =
                    """
                    grid-row-end: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-row-end-2",
                EscapedClassName = ".-row-end-2",
                Styles =
                    """
                    grid-row-end: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-end-[3]",
                EscapedClassName = @".row-end-\[3\]",
                Styles =
                    """
                    grid-row-end: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-end-(--my-span)",
                EscapedClassName = @".row-end-\(--my-span\)",
                Styles =
                    """
                    grid-row-end: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-end-[var(--my-span)]",
                EscapedClassName = @".row-end-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-row-end: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region row
            
            new ()
            {
                ClassName = "row-auto",
                EscapedClassName = ".row-auto",
                Styles =
                    """
                    grid-row: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-2",
                EscapedClassName = ".row-2",
                Styles =
                    """
                    grid-row: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-row-2",
                EscapedClassName = ".-row-2",
                Styles =
                    """
                    grid-row: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-[3]",
                EscapedClassName = @".row-\[3\]",
                Styles =
                    """
                    grid-row: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-(--my-span)",
                EscapedClassName = @".row-\(--my-span\)",
                Styles =
                    """
                    grid-row: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "row-[var(--my-span)]",
                EscapedClassName = @".row-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-row: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
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
