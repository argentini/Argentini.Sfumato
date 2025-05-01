namespace Argentini.Sfumato.Tests.UtilityClasses.Spacing;

public class SpaceTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Space()
    {
        var appRunner = new AppRunner(new AppState());
        
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "space-x-px",
                EscapedClassName = ".space-x-px",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(1px * var(--sf-space-x-reverse));
                        margin-inline-end: calc(1px * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "-space-x-px",
                EscapedClassName = ".-space-x-px",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(-1px * var(--sf-space-x-reverse));
                        margin-inline-end: calc(-1px * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-3/4",
                EscapedClassName = @".space-x-3\/4",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(75% * var(--sf-space-x-reverse));
                        margin-inline-end: calc(75% * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-5",
                EscapedClassName = ".space-x-5",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(calc(var(--spacing) * 5) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(calc(var(--spacing) * 5) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "-space-x-5",
                EscapedClassName = ".-space-x-5",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(calc(var(--spacing) * -5) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(calc(var(--spacing) * -5) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-[1.25rem]",
                EscapedClassName = @".space-x-\[1\.25rem\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(1.25rem * var(--sf-space-x-reverse));
                        margin-inline-end: calc(1.25rem * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-(--my-margin)",
                EscapedClassName = @".space-x-\(--my-margin\)",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(var(--my-margin) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(var(--my-margin) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "-space-x-(--my-margin)",
                EscapedClassName = @".-space-x-\(--my-margin\)",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(-1 * var(--my-margin) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(-1 * var(--my-margin) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-(length:--my-margin)",
                EscapedClassName = @".space-x-\(length\:--my-margin\)",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(var(--my-margin) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(var(--my-margin) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-[var(--my-margin)]",
                EscapedClassName = @".space-x-\[var\(--my-margin\)\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(var(--my-margin) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(var(--my-margin) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
            new ()
            {
                ClassName = "space-x-[length:var(--my-margin)]",
                EscapedClassName = @".space-x-\[length\:var\(--my-margin\)\]",
                Styles =
                    """
                    & > :not(:last-child) {
                        margin-inline-start: calc(var(--my-margin) * var(--sf-space-x-reverse));
                        margin-inline-end: calc(var(--my-margin) * calc(1 - var(--sf-space-x-reverse)));
                    };
                    """,
                IsValid = true,
                IsImportant = false,
                UsedCssCustomProperties = [ "--spacing", "--sf-space-x-reverse" ]
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.UsedCssCustomProperties.Length, cssClass.UsesCssCustomProperties.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.UsedCssCustomProperties.Length; i++)
            {
                Assert.Equal(test.UsedCssCustomProperties.ElementAt(i), cssClass.UsesCssCustomProperties.ElementAt(i));
            }
            
            testOutputHelper.WriteLine($"{GetType().Name} => {test.ClassName}");
        }
    }
}
