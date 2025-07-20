namespace Argentini.Sfumato.Tests.UtilityClasses.Transforms;

public class TranslateTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void Translate()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "translate-none",
                EscapedClassName = ".translate-none",
                Styles =
                    """
                    translate: none;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-full",
                EscapedClassName = ".translate-full",
                Styles =
                    """
                    translate: 100% 100%;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-px",
                EscapedClassName = ".translate-px",
                Styles =
                    """
                    translate: 1px 1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-translate-px",
                EscapedClassName = ".-translate-px",
                Styles =
                    """
                    translate: -1px -1px;
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-37",
                EscapedClassName = ".translate-37",
                Styles =
                    """
                    --sf-translate-x: calc(var(--spacing) * 37);
                    --sf-translate-y: calc(var(--spacing) * 37);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-translate-37",
                EscapedClassName = ".-translate-37",
                Styles =
                    """
                    --sf-translate-x: calc(var(--spacing) * -37);
                    --sf-translate-y: calc(var(--spacing) * -37);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-[1.25rem]",
                EscapedClassName = @".translate-\[1\.25rem\]",
                Styles =
                    """
                    --sf-translate-x: 1.25rem;
                    --sf-translate-y: 1.25rem;
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-1/2",
                EscapedClassName = @".translate-1\/2",
                Styles =
                    """
                    --sf-translate-x: 50%;
                    --sf-translate-y: 50%;
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-[var(--my-angle)]",
                EscapedClassName = @".translate-\[var\(--my-angle\)\]",
                Styles =
                    """
                    --sf-translate-x: var(--my-angle);
                    --sf-translate-y: var(--my-angle);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-(--my-angle)",
                EscapedClassName = @".translate-\(--my-angle\)",
                Styles =
                    """
                    --sf-translate-x: var(--my-angle);
                    --sf-translate-y: var(--my-angle);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            
            
            
            new ()
            {
                ClassName = "translate-x-full",
                EscapedClassName = ".translate-x-full",
                Styles =
                    """
                    translate: 100% var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-px",
                EscapedClassName = ".translate-x-px",
                Styles =
                    """
                    translate: 1px var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-translate-x-px",
                EscapedClassName = ".-translate-x-px",
                Styles =
                    """
                    translate: -1px var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-37",
                EscapedClassName = ".translate-x-37",
                Styles =
                    """
                    --sf-translate-x: calc(var(--spacing) * 37);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "-translate-x-37",
                EscapedClassName = ".-translate-x-37",
                Styles =
                    """
                    --sf-translate-x: calc(var(--spacing) * -37);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-[1.25rem]",
                EscapedClassName = @".translate-x-\[1\.25rem\]",
                Styles =
                    """
                    --sf-translate-x: 1.25rem;
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-1/2",
                EscapedClassName = @".translate-x-1\/2",
                Styles =
                    """
                    --sf-translate-x: 50%;
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-[var(--my-angle)]",
                EscapedClassName = @".translate-x-\[var\(--my-angle\)\]",
                Styles =
                    """
                    --sf-translate-x: var(--my-angle);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
                    """,
                IsValid = true,
                IsImportant = false,
            },
            new ()
            {
                ClassName = "translate-x-(--my-angle)",
                EscapedClassName = @".translate-x-\(--my-angle\)",
                Styles =
                    """
                    --sf-translate-x: var(--my-angle);
                    translate: var(--sf-translate-x) var(--sf-translate-y);
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
