namespace Sfumato.Tests;

/// <summary>
/// Tests for the <c>in-*</c> "implicit group" variants (e.g. <c>in-hover:</c>).
/// These style an element based on the state of an ancestor without requiring a
/// <c>.group</c> class. The generated selector is <c>:where(&lt;state&gt;) .in-&lt;state&gt;\:&lt;utility&gt;</c>.
/// </summary>
public sealed class ImplicitGroupInVariantTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ImplicitGroupStateVariants()
    {
        var testClasses = new List<TestClass>
        {
            // Interactive / hover states
            new() { ClassName = "in-hover:flex",        EscapedClassName = @":where(:hover) .in-hover\:flex" },
            new() { ClassName = "in-focus:flex",        EscapedClassName = @":where(:focus) .in-focus\:flex" },
            new() { ClassName = "in-focus-within:flex", EscapedClassName = @":where(:focus-within) .in-focus-within\:flex" },
            new() { ClassName = "in-focus-visible:flex",EscapedClassName = @":where(:focus-visible) .in-focus-visible\:flex" },
            new() { ClassName = "in-active:flex",       EscapedClassName = @":where(:active) .in-active\:flex" },

            // Form control states
            new() { ClassName = "in-checked:flex",      EscapedClassName = @":where(:checked) .in-checked\:flex" },
            new() { ClassName = "in-disabled:flex",     EscapedClassName = @":where(:disabled) .in-disabled\:flex" },
            new() { ClassName = "in-enabled:flex",      EscapedClassName = @":where(:enabled) .in-enabled\:flex" },
            new() { ClassName = "in-required:flex",     EscapedClassName = @":where(:required) .in-required\:flex" },
            new() { ClassName = "in-valid:flex",        EscapedClassName = @":where(:valid) .in-valid\:flex" },
            new() { ClassName = "in-invalid:flex",      EscapedClassName = @":where(:invalid) .in-invalid\:flex" },
            new() { ClassName = "in-in-range:flex",     EscapedClassName = @":where(:in-range) .in-in-range\:flex" },
            new() { ClassName = "in-out-of-range:flex", EscapedClassName = @":where(:out-of-range) .in-out-of-range\:flex" },
            new() { ClassName = "in-read-only:flex",    EscapedClassName = @":where(:read-only) .in-read-only\:flex" },
            new() { ClassName = "in-indeterminate:flex",EscapedClassName = @":where(:indeterminate) .in-indeterminate\:flex" },
            new() { ClassName = "in-default:flex",      EscapedClassName = @":where(:default) .in-default\:flex" },
            new() { ClassName = "in-placeholder-shown:flex", EscapedClassName = @":where(:placeholder-shown) .in-placeholder-shown\:flex" },
            new() { ClassName = "in-autofill:flex",     EscapedClassName = @":where(:autofill) .in-autofill\:flex" },

            // Structural / positional states
            new() { ClassName = "in-first:flex",        EscapedClassName = @":where(:first-child) .in-first\:flex" },
            new() { ClassName = "in-last:flex",         EscapedClassName = @":where(:last-child) .in-last\:flex" },
            new() { ClassName = "in-only:flex",         EscapedClassName = @":where(:only-child) .in-only\:flex" },
            new() { ClassName = "in-odd:flex",          EscapedClassName = @":where(:nth-child(odd)) .in-odd\:flex" },
            new() { ClassName = "in-even:flex",         EscapedClassName = @":where(:nth-child(even)) .in-even\:flex" },
            new() { ClassName = "in-first-of-type:flex",EscapedClassName = @":where(:first-of-type) .in-first-of-type\:flex" },
            new() { ClassName = "in-last-of-type:flex", EscapedClassName = @":where(:last-of-type) .in-last-of-type\:flex" },
            new() { ClassName = "in-only-of-type:flex", EscapedClassName = @":where(:only-of-type) .in-only-of-type\:flex" },
            new() { ClassName = "in-empty:flex",        EscapedClassName = @":where(:empty) .in-empty\:flex" },

            // Link / document states
            new() { ClassName = "in-visited:flex",      EscapedClassName = @":where(:visited) .in-visited\:flex" },
            new() { ClassName = "in-target:flex",       EscapedClassName = @":where(:target) .in-target\:flex" },

            // Popover / dialog states
            new() { ClassName = "in-open:flex",         EscapedClassName = @":where(:is([open],:popover-open,:open)) .in-open\:flex" },
            new() { ClassName = "in-closed:flex",       EscapedClassName = @":where(:is([closed],:popover-closed,:closed)) .in-closed\:flex" },
        };

        foreach (var test in testClasses)
        {
            using var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.True(cssClass.IsValid, test.ClassName);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);

            TestOutputHelper?.WriteLine($"ImplicitGroupStateVariants() => {test.ClassName}");
        }
    }

    [Fact]
    public void ImplicitGroupDataAndAriaVariants()
    {
        var testClasses = new List<TestClass>
        {
            // Data attributes (bare and arbitrary)
            new() { ClassName = "in-data-active:flex",     EscapedClassName = @":where([data-active]) .in-data-active\:flex" },
            new() { ClassName = "in-data-[state=open]:flex", EscapedClassName = @":where([state=open]) .in-data-\[state\=open\]\:flex" },

            // ARIA states
            new() { ClassName = "in-aria-checked:flex",    EscapedClassName = @":where([aria-checked=""true""]) .in-aria-checked\:flex" },
            new() { ClassName = "in-aria-expanded:flex",   EscapedClassName = @":where([aria-expanded=""true""]) .in-aria-expanded\:flex" },
            new() { ClassName = "in-aria-hidden:flex",     EscapedClassName = @":where([aria-hidden=""true""]) .in-aria-hidden\:flex" },
            new() { ClassName = "in-aria-pressed:flex",    EscapedClassName = @":where([aria-pressed=""true""]) .in-aria-pressed\:flex" },
        };

        foreach (var test in testClasses)
        {
            using var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.True(cssClass.IsValid, test.ClassName);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);

            TestOutputHelper?.WriteLine($"ImplicitGroupDataAndAriaVariants() => {test.ClassName}");
        }
    }

    [Fact]
    public void ImplicitGroupNumericSuffixVariants()
    {
        var testClasses = new List<TestClass>
        {
            new() { ClassName = "in-nth-2:flex", EscapedClassName = @":where(:nth-child(2)) .in-nth-2\:flex" },
            new() { ClassName = "in-nth-3:flex", EscapedClassName = @":where(:nth-child(3)) .in-nth-3\:flex" },
        };

        foreach (var test in testClasses)
        {
            using var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.True(cssClass.IsValid, test.ClassName);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);

            TestOutputHelper?.WriteLine($"ImplicitGroupNumericSuffixVariants() => {test.ClassName}");
        }
    }

    [Fact]
    public void ImplicitGroupComposesWithOtherVariants()
    {
        // Stacked after a media query variant.
        using (var cssClass = new CssClass(AppRunner, selector: "dark:in-hover:flex"))
        {
            Assert.True(cssClass.IsValid);
            Assert.Equal(@":where(:hover) .dark\:in-hover\:flex", cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);
        }

        // Stacked after a breakpoint variant with a different utility.
        using (var cssClass = new CssClass(AppRunner, selector: "md:in-focus:text-red-500"))
        {
            Assert.True(cssClass.IsValid);
            Assert.Equal(@":where(:focus) .md\:in-focus\:text-red-500", cssClass.EscapedSelector);
            Assert.Equal("color: var(--color-red-500);", cssClass.Styles);
        }

        // Important modifier.
        using (var cssClass = new CssClass(AppRunner, selector: "in-hover:flex!"))
        {
            Assert.True(cssClass.IsValid);
            Assert.True(cssClass.IsImportant);
            Assert.Equal(@":where(:hover) .in-hover\:flex\!", cssClass.EscapedSelector);
            Assert.Equal("display: flex !important;", cssClass.Styles);
        }

        // Stacked with a pseudo-class variant on the same element.
        using (var cssClass = new CssClass(AppRunner, selector: "in-hover:hover:flex"))
        {
            Assert.True(cssClass.IsValid);
            Assert.Equal(@":where(:hover) .in-hover\:hover\:flex:hover", cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);
        }
    }

    [Fact]
    public void ImplicitGroupRejectsIncompatibleVariants()
    {
        var invalidCandidates = new[]
        {
            // in-not-* is not supported (Tailwind emits nothing for it).
            "in-not-hover:flex",
            "in-not-focus:flex",

            // Wrapper / media-query variants are not selector-based.
            "in-dark:flex",
            "in-sm:flex",
            "in-motion-safe:flex",

            // Non-variant utility names.
            "in-flex:flex",
            "in-block:flex",

            // Modifiers are not allowed on in-*.
            "in-hover/foo:flex",

            // Pseudo-elements are not compatible (Tailwind emits nothing for them).
            "in-before:flex",
            "in-after:flex",
            "in-first-letter:flex",
            "in-first-line:flex",
            "in-selection:flex",
            "in-placeholder:flex",
            "in-backdrop:flex",

            // Nested in-* is not supported.
            "in-in-hover:flex",
        };

        foreach (var candidate in invalidCandidates)
        {
            using var cssClass = new CssClass(AppRunner, selector: candidate);

            Assert.False(cssClass.IsValid, candidate);
            Assert.Equal(string.Empty, cssClass.EscapedSelector);

            TestOutputHelper?.WriteLine($"ImplicitGroupRejectsIncompatibleVariants() => {candidate} (rejected)");
        }
    }
}
