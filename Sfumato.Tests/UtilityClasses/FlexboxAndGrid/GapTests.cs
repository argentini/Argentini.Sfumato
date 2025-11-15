namespace Sfumato.Tests.UtilityClasses.FlexboxAndGrid;

public class GapTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Gap()
    {
        var testClasses = new List<TestClass>()
        {
            #region gap
            
            new ()
            {
                ClassName = "gap-3/4",
                EscapedClassName = @".gap-3\/4",
                Styles =
                    """
                    gap: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-5",
                EscapedClassName = ".gap-5",
                Styles =
                    """
                    gap: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-[1.25rem]",
                EscapedClassName = @".gap-\[1\.25rem\]",
                Styles =
                    """
                    gap: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-(--my-gap)",
                EscapedClassName = @".gap-\(--my-gap\)",
                Styles =
                    """
                    gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-(length:--my-gap)",
                EscapedClassName = @".gap-\(length\:--my-gap\)",
                Styles =
                    """
                    gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-[var(--my-gap)]",
                EscapedClassName = @".gap-\[var\(--my-gap\)\]",
                Styles =
                    """
                    gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-[length:var(--my-gap)]",
                EscapedClassName = @".gap-\[length\:var\(--my-gap\)\]",
                Styles =
                    """
                    gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion
            
            #region gap-x
            
            new ()
            {
                ClassName = "gap-x-3/4",
                EscapedClassName = @".gap-x-3\/4",
                Styles =
                    """
                    column-gap: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-5",
                EscapedClassName = ".gap-x-5",
                Styles =
                    """
                    column-gap: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-[1.25rem]",
                EscapedClassName = @".gap-x-\[1\.25rem\]",
                Styles =
                    """
                    column-gap: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-(--my-gap)",
                EscapedClassName = @".gap-x-\(--my-gap\)",
                Styles =
                    """
                    column-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-(length:--my-gap)",
                EscapedClassName = @".gap-x-\(length\:--my-gap\)",
                Styles =
                    """
                    column-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-[var(--my-gap)]",
                EscapedClassName = @".gap-x-\[var\(--my-gap\)\]",
                Styles =
                    """
                    column-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-x-[length:var(--my-gap)]",
                EscapedClassName = @".gap-x-\[length\:var\(--my-gap\)\]",
                Styles =
                    """
                    column-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            #endregion

            #region gap-y
            
            new ()
            {
                ClassName = "gap-y-3/4",
                EscapedClassName = @".gap-y-3\/4",
                Styles =
                    """
                    row-gap: 75%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-5",
                EscapedClassName = ".gap-y-5",
                Styles =
                    """
                    row-gap: calc(var(--spacing) * 5);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-[1.25rem]",
                EscapedClassName = @".gap-y-\[1\.25rem\]",
                Styles =
                    """
                    row-gap: 1.25rem;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-(--my-gap)",
                EscapedClassName = @".gap-y-\(--my-gap\)",
                Styles =
                    """
                    row-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-(length:--my-gap)",
                EscapedClassName = @".gap-y-\(length\:--my-gap\)",
                Styles =
                    """
                    row-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-[var(--my-gap)]",
                EscapedClassName = @".gap-y-\[var\(--my-gap\)\]",
                Styles =
                    """
                    row-gap: var(--my-gap);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "gap-y-[length:var(--my-gap)]",
                EscapedClassName = @".gap-y-\[length\:var\(--my-gap\)\]",
                Styles =
                    """
                    row-gap: var(--my-gap);
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
