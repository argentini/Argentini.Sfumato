namespace Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GridColumnTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void GridColumn()
    {
        var testClasses = new List<TestClass>()
        {
            #region col-span
            
            new ()
            {
                ClassName = "col-span-full",
                EscapedClassName = ".col-span-full",
                Styles =
                    """
                    grid-column: 1 / -1;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-span-2",
                EscapedClassName = ".col-span-2",
                Styles =
                    """
                    grid-column: span 2 / span 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-span-[3]",
                EscapedClassName = @".col-span-\[3\]",
                Styles =
                    """
                    grid-column: span 3 / span 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-span-(--my-span)",
                EscapedClassName = @".col-span-\(--my-span\)",
                Styles =
                    """
                    grid-column: span var(--my-span) / span var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-span-[var(--my-span)]",
                EscapedClassName = @".col-span-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-column: span var(--my-span) / span var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region col-start
            
            new ()
            {
                ClassName = "col-start-auto",
                EscapedClassName = ".col-start-auto",
                Styles =
                    """
                    grid-column-start: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-start-2",
                EscapedClassName = ".col-start-2",
                Styles =
                    """
                    grid-column-start: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-col-start-2",
                EscapedClassName = ".-col-start-2",
                Styles =
                    """
                    grid-column-start: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-start-[3]",
                EscapedClassName = @".col-start-\[3\]",
                Styles =
                    """
                    grid-column-start: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-start-(--my-span)",
                EscapedClassName = @".col-start-\(--my-span\)",
                Styles =
                    """
                    grid-column-start: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-start-[var(--my-span)]",
                EscapedClassName = @".col-start-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-column-start: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion

            #region col-end
            
            new ()
            {
                ClassName = "col-end-auto",
                EscapedClassName = ".col-end-auto",
                Styles =
                    """
                    grid-column-end: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-end-2",
                EscapedClassName = ".col-end-2",
                Styles =
                    """
                    grid-column-end: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-col-end-2",
                EscapedClassName = ".-col-end-2",
                Styles =
                    """
                    grid-column-end: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-end-[3]",
                EscapedClassName = @".col-end-\[3\]",
                Styles =
                    """
                    grid-column-end: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-end-(--my-span)",
                EscapedClassName = @".col-end-\(--my-span\)",
                Styles =
                    """
                    grid-column-end: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-end-[var(--my-span)]",
                EscapedClassName = @".col-end-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-column-end: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region col
            
            new ()
            {
                ClassName = "col-auto",
                EscapedClassName = ".col-auto",
                Styles =
                    """
                    grid-column: auto;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-2",
                EscapedClassName = ".col-2",
                Styles =
                    """
                    grid-column: 2;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-col-2",
                EscapedClassName = ".-col-2",
                Styles =
                    """
                    grid-column: calc(2 * -1);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-[3]",
                EscapedClassName = @".col-\[3\]",
                Styles =
                    """
                    grid-column: 3;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-(--my-span)",
                EscapedClassName = @".col-\(--my-span\)",
                Styles =
                    """
                    grid-column: var(--my-span);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "col-[var(--my-span)]",
                EscapedClassName = @".col-\[var\(--my-span\)\]",
                Styles =
                    """
                    grid-column: var(--my-span);
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
