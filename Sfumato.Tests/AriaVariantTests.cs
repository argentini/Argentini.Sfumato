namespace Sfumato.Tests;

/// <summary>
/// Tests for the arbitrary-value ARIA variant family: <c>aria-[...]</c> (e.g.
/// <c>aria-[sort=ascending]:</c>) and its related forms (<c>group-aria-*</c>,
/// <c>peer-aria-*</c>, <c>not-aria-*</c>, <c>in-aria-*</c>).
/// Expected selectors are verified against Tailwind CSS v4.3.3 output.
/// </summary>
public sealed class AriaVariantTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void ArbitraryAriaVariants()
    {
        var testClasses = new List<TestClass>
        {
            // attr=value -> [aria-attr="value"]
            new() { ClassName = "aria-[sort=ascending]:flex",   EscapedClassName = @".aria-\[sort\=ascending\]\:flex[aria-sort=""ascending""]" },
            new() { ClassName = "aria-[checked=mixed]:flex",    EscapedClassName = @".aria-\[checked\=mixed\]\:flex[aria-checked=""mixed""]" },
            new() { ClassName = "aria-[disabled=false]:flex",   EscapedClassName = @".aria-\[disabled\=false\]\:flex[aria-disabled=""false""]" },
            new() { ClassName = "aria-[orientation=horizontal]:flex", EscapedClassName = @".aria-\[orientation\=horizontal\]\:flex[aria-orientation=""horizontal""]" },

            // attr (presence, no value) -> [aria-attr]
            new() { ClassName = "aria-[current]:flex",          EscapedClassName = @".aria-\[current\]\:flex[aria-current]" },
            new() { ClassName = "aria-[hidden]:flex",           EscapedClassName = @".aria-\[hidden\]\:flex[aria-hidden]" },

            // Underscores become spaces in the value.
            new() { ClassName = "aria-[label=hello_world]:flex", EscapedClassName = @".aria-\[label\=hello_world\]\:flex[aria-label=""hello world""]" },

            // Degenerate empty attribute name (matches Tailwind output).
            new() { ClassName = "aria-[=foo]:flex",             EscapedClassName = @".aria-\[\=foo\]\:flex[aria-=""foo""]" },
        };

        AssertValidAria(testClasses);
    }

    [Fact]
    public void ArbitraryAriaInvalidVariants()
    {
        var invalidCandidates = new[]
        {
            "aria-[]:flex",          // empty value
            "aria-foo:flex",         // unknown attribute, not a standard variant
            "aria-sort:flex",        // custom aria variants require @custom-variant
            "aria-[sort]:foo:flex",  // (sanity: unknown utility)
        };

        AssertInvalidAria(invalidCandidates);
    }

    [Fact]
    public void StandardAriaVariantsStillResolve()
    {
        // Regression: the built-in boolean aria-* variants are exact matches and must be unaffected.
        var testClasses = new List<TestClass>
        {
            new() { ClassName = "aria-busy:flex",      EscapedClassName = @".aria-busy\:flex[aria-busy=""true""]" },
            new() { ClassName = "aria-checked:flex",   EscapedClassName = @".aria-checked\:flex[aria-checked=""true""]" },
            new() { ClassName = "aria-disabled:flex",  EscapedClassName = @".aria-disabled\:flex[aria-disabled=""true""]" },
            new() { ClassName = "aria-expanded:flex",  EscapedClassName = @".aria-expanded\:flex[aria-expanded=""true""]" },
            new() { ClassName = "aria-hidden:flex",    EscapedClassName = @".aria-hidden\:flex[aria-hidden=""true""]" },
            new() { ClassName = "aria-pressed:flex",   EscapedClassName = @".aria-pressed\:flex[aria-pressed=""true""]" },
            new() { ClassName = "aria-readonly:flex",  EscapedClassName = @".aria-readonly\:flex[aria-readonly=""true""]" },
            new() { ClassName = "aria-required:flex",  EscapedClassName = @".aria-required\:flex[aria-required=""true""]" },
            new() { ClassName = "aria-selected:flex",  EscapedClassName = @".aria-selected\:flex[aria-selected=""true""]" },
        };

        AssertValidAria(testClasses);
    }

    [Fact]
    public void GroupArbitraryAriaVariants()
    {
        var testClasses = new List<TestClass>
        {
            // Arbitrary: group-aria-[sort=ascending] -> :is(:where(.group)[aria-sort="ascending"] *)
            new() { ClassName = "group-aria-[sort=ascending]:flex", EscapedClassName = @".group-aria-\[sort\=ascending\]\:flex:is(:where(.group)[aria-sort=""ascending""] *)" },
            new() { ClassName = "group-aria-[label=a_b]:flex",      EscapedClassName = @".group-aria-\[label\=a_b\]\:flex:is(:where(.group)[aria-label=""a b""] *)" },

            // Standard (previously resolved to the wrong :checked selector):
            new() { ClassName = "group-aria-checked:flex",  EscapedClassName = @".group-aria-checked\:flex:is(:where(.group)[aria-checked=""true""] *)" },
            new() { ClassName = "group-aria-hidden:flex",   EscapedClassName = @".group-aria-hidden\:flex:is(:where(.group)[aria-hidden=""true""] *)" },
            new() { ClassName = "group-aria-expanded:flex", EscapedClassName = @".group-aria-expanded\:flex:is(:where(.group)[aria-expanded=""true""] *)" },
        };

        AssertValidAria(testClasses);

        // Unknown attribute is rejected.
        AssertInvalidAria(new[] { "group-aria-foo:flex" });
    }

    [Fact]
    public void PeerArbitraryAriaVariants()
    {
        var testClasses = new List<TestClass>
        {
            // Arbitrary: peer-aria-[sort=descending] -> :is(:where(.peer)[aria-sort="descending"] ~ *)
            new() { ClassName = "peer-aria-[sort=descending]:flex", EscapedClassName = @".peer-aria-\[sort\=descending\]\:flex:is(:where(.peer)[aria-sort=""descending""] ~ *)" },
            new() { ClassName = "peer-aria-[label=a_b]:flex",       EscapedClassName = @".peer-aria-\[label\=a_b\]\:flex:is(:where(.peer)[aria-label=""a b""] ~ *)" },

            // Standard (previously invalid because the bare value was looked up as a pseudo-class):
            new() { ClassName = "peer-aria-checked:flex",   EscapedClassName = @".peer-aria-checked\:flex:is(:where(.peer)[aria-checked=""true""] ~ *)" },
            new() { ClassName = "peer-aria-expanded:flex",  EscapedClassName = @".peer-aria-expanded\:flex:is(:where(.peer)[aria-expanded=""true""] ~ *)" },
            new() { ClassName = "peer-aria-selected:flex",  EscapedClassName = @".peer-aria-selected\:flex:is(:where(.peer)[aria-selected=""true""] ~ *)" },
        };

        AssertValidAria(testClasses);

        // Unknown attribute is rejected.
        AssertInvalidAria(new[] { "peer-aria-foo:flex" });
    }

    [Fact]
    public void NotArbitraryAriaVariants()
    {
        var testClasses = new List<TestClass>
        {
            // Arbitrary: not-aria-[sort=ascending] -> :not([aria-sort="ascending"])
            new() { ClassName = "not-aria-[sort=ascending]:flex", EscapedClassName = @".not-aria-\[sort\=ascending\]\:flex:not([aria-sort=""ascending""])" },
            new() { ClassName = "not-aria-[current]:flex",        EscapedClassName = @".not-aria-\[current\]\:flex:not([aria-current])" },

            // Standard (auto-generated negated companions):
            new() { ClassName = "not-aria-checked:flex",  EscapedClassName = @".not-aria-checked\:flex:not([aria-checked=""true""])" },
            new() { ClassName = "not-aria-expanded:flex", EscapedClassName = @".not-aria-expanded\:flex:not([aria-expanded=""true""])" },
        };

        AssertValidAria(testClasses);

        // Unknown attribute is rejected.
        AssertInvalidAria(new[] { "not-aria-foo:flex" });
    }

    [Fact]
    public void ImplicitGroupArbitraryAriaVariants()
    {
        // in-aria-[...] resolves the inner aria variant and emits a :where(...) prefix.
        var testClasses = new List<TestClass>
        {
            new() { ClassName = "in-aria-[sort=ascending]:flex", EscapedClassName = @":where([aria-sort=""ascending""]) .in-aria-\[sort\=ascending\]\:flex" },
            new() { ClassName = "in-aria-[current]:flex",        EscapedClassName = @":where([aria-current]) .in-aria-\[current\]\:flex" },

            // Standard in-aria-* (regression):
            new() { ClassName = "in-aria-checked:flex",  EscapedClassName = @":where([aria-checked=""true""]) .in-aria-checked\:flex" },
            new() { ClassName = "in-aria-expanded:flex", EscapedClassName = @":where([aria-expanded=""true""]) .in-aria-expanded\:flex" },
        };

        AssertValidAria(testClasses);
    }

    [Fact]
    public void ArbitraryAriaComposesWithOtherVariants()
    {
        // Stacked after a media query variant.
        using (var cssClass = new CssClass(AppRunner, selector: "dark:aria-[sort=ascending]:flex"))
        {
            Assert.True(cssClass.IsValid);
            Assert.Equal(@".dark\:aria-\[sort\=ascending\]\:flex[aria-sort=""ascending""]", cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);
        }

        // Stacked with a pseudo-class variant on the same element (order preserved).
        using (var cssClass = new CssClass(AppRunner, selector: "hover:aria-[checked=mixed]:flex"))
        {
            Assert.True(cssClass.IsValid);
            Assert.Equal(@".hover\:aria-\[checked\=mixed\]\:flex:hover[aria-checked=""mixed""]", cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);
        }

        // Important modifier.
        using (var cssClass = new CssClass(AppRunner, selector: "aria-[sort=ascending]:flex!"))
        {
            Assert.True(cssClass.IsValid);
            Assert.True(cssClass.IsImportant);
            Assert.Equal(@".aria-\[sort\=ascending\]\:flex\![aria-sort=""ascending""]", cssClass.EscapedSelector);
            Assert.Equal("display: flex !important;", cssClass.Styles);
        }
    }

    [Fact]
    public void ArbitraryAriaIsScannedFromMarkupAndCompiled()
    {
        // End-to-end: the content scanner must pick up aria-[...] classes from HTML and
        // compile them to the correct selectors (verified against Tailwind v4.3.3).
        const string markup = """
            <table>
              <th aria-sort="ascending" class="group aria-[sort=ascending]:underline peer-aria-[sort=descending]:font-bold not-aria-[current]:italic in-aria-[checked=mixed]:text-red-500">
                Invoice #
              </th>
            </table>
            """;

        var utilityClasses = Sfumato.Entities.Scanning.ContentScanner.ScanFileForUtilityClasses(markup, AppRunner);

        Assert.True(utilityClasses.ContainsKey("aria-[sort=ascending]:underline"), "scanner missed aria-[...]");
        Assert.Equal(@".aria-\[sort\=ascending\]\:underline[aria-sort=""ascending""]", utilityClasses["aria-[sort=ascending]:underline"].EscapedSelector);

        Assert.True(utilityClasses.ContainsKey("peer-aria-[sort=descending]:font-bold"), "scanner missed peer-aria-[...]");
        Assert.Equal(@".peer-aria-\[sort\=descending\]\:font-bold:is(:where(.peer)[aria-sort=""descending""] ~ *)", utilityClasses["peer-aria-[sort=descending]:font-bold"].EscapedSelector);

        Assert.True(utilityClasses.ContainsKey("not-aria-[current]:italic"), "scanner missed not-aria-[...]");
        Assert.Equal(@".not-aria-\[current\]\:italic:not([aria-current])", utilityClasses["not-aria-[current]:italic"].EscapedSelector);

        Assert.True(utilityClasses.ContainsKey("in-aria-[checked=mixed]:text-red-500"), "scanner missed in-aria-[...]");
        Assert.Equal(@":where([aria-checked=""mixed""]) .in-aria-\[checked\=mixed\]\:text-red-500", utilityClasses["in-aria-[checked=mixed]:text-red-500"].EscapedSelector);
    }

    private void AssertValidAria(List<TestClass> testClasses)
    {
        foreach (var test in testClasses)
        {
            using var cssClass = new CssClass(AppRunner, selector: test.ClassName);

            Assert.True(cssClass.IsValid, test.ClassName);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal("display: flex;", cssClass.Styles);

            TestOutputHelper?.WriteLine($"AriaVariantTests() => {test.ClassName}");
        }
    }

    private void AssertInvalidAria(string[] invalidCandidates)
    {
        foreach (var candidate in invalidCandidates)
        {
            using var cssClass = new CssClass(AppRunner, selector: candidate);

            Assert.False(cssClass.IsValid, candidate);
            Assert.Equal(string.Empty, cssClass.EscapedSelector);

            TestOutputHelper?.WriteLine($"AriaVariantTests() => {candidate} (rejected)");
        }
    }
}
