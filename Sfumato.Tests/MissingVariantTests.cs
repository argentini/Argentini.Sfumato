namespace Sfumato.Tests;

/// <summary>
/// Verifies the Tailwind CSS v4.3.3 syntax supported by the optional,
/// user-valid, user-invalid, and details-content variants.
/// </summary>
public sealed class MissingVariantTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    private static readonly (string Name, string Suffix)[] FormStateVariants =
    [
        ("optional", ":optional"),
        ("user-valid", ":user-valid"),
        ("user-invalid", ":user-invalid"),
    ];

    [Fact]
    public void FormStateVariantsSupportEverySyntaxPermutation()
    {
        for (var i = 0; i < FormStateVariants.Length; i++)
        {
            var (name, suffix) = FormStateVariants[i];

            AssertSelector($"{name}:flex", $".{name}\\:flex{suffix}");
            AssertSelector($"not-{name}:flex", $".not-{name}\\:flex:not({suffix})");
            AssertSelector($"group-{name}:flex", $".group{suffix} .group-{name}\\:flex");
            AssertSelector($"group-{name}/field:flex", $".group\\/field{suffix} .group-{name}\\/field\\:flex");
            AssertSelector($"peer-{name}:flex", $".peer{suffix} ~ .peer-{name}\\:flex");
            AssertSelector($"peer-{name}/field:flex", $".peer\\/field{suffix} ~ .peer-{name}\\/field\\:flex");
            AssertSelector($"has-{name}:flex", $".has-{name}\\:flex:has({suffix})");
            AssertSelector($"in-{name}:flex", $":where({suffix}) .in-{name}\\:flex");
        }
    }

    [Fact]
    public void FormStateVariantsSupportBreakpointAndImportantSyntax()
    {
        for (var i = 0; i < FormStateVariants.Length; i++)
        {
            var (name, suffix) = FormStateVariants[i];

            using (var breakpoint = new CssClass(AppRunner, selector: $"md:{name}:flex"))
            {
                Assert.True(breakpoint.IsValid, name);
                Assert.Equal($".md\\:{name}\\:flex{suffix}", breakpoint.EscapedSelector);
                Assert.Contains($"@media {AppRunner.Library.MediaQueryPrefixes["md"].Statement} {{", breakpoint.Wrappers.Values);
                Assert.Equal("display: flex;", breakpoint.Styles);
            }

            using (var important = new CssClass(AppRunner, selector: $"{name}:flex!"))
            {
                Assert.True(important.IsValid, name);
                Assert.True(important.IsImportant, name);
                Assert.Equal($".{name}\\:flex\\!{suffix}", important.EscapedSelector);
                Assert.Equal("display: flex !important;", important.Styles);
            }
        }
    }

    [Fact]
    public void FormStateVariantsRejectUnsupportedModifiers()
    {
        for (var i = 0; i < FormStateVariants.Length; i++)
        {
            var name = FormStateVariants[i].Name;

            AssertInvalid($"{name}/field:flex");
            AssertInvalid($"not-{name}/field:flex");
            AssertInvalid($"has-{name}/field:flex");
            AssertInvalid($"in-{name}/field:flex");
        }
    }

    [Fact]
    public void DetailsContentSupportsDirectBreakpointAndImportantSyntax()
    {
        AssertSelector("details-content:flex", @".details-content\:flex::details-content");

        using (var breakpoint = new CssClass(AppRunner, selector: "md:details-content:flex"))
        {
            Assert.True(breakpoint.IsValid);
            Assert.Equal(@".md\:details-content\:flex::details-content", breakpoint.EscapedSelector);
            Assert.Contains($"@media {AppRunner.Library.MediaQueryPrefixes["md"].Statement} {{", breakpoint.Wrappers.Values);
            Assert.Equal("display: flex;", breakpoint.Styles);
        }

        using var important = new CssClass(AppRunner, selector: "details-content:flex!");

        Assert.True(important.IsValid);
        Assert.True(important.IsImportant);
        Assert.Equal(@".details-content\:flex\!::details-content", important.EscapedSelector);
        Assert.Equal("display: flex !important;", important.Styles);
    }

    [Fact]
    public void DetailsContentRejectsUnsupportedCompoundSyntax()
    {
        string[] invalidCandidates =
        [
            "not-details-content:flex",
            "group-details-content:flex",
            "group-details-content/field:flex",
            "peer-details-content:flex",
            "peer-details-content/field:flex",
            "has-details-content:flex",
            "in-details-content:flex",
            "details-content/field:flex",
        ];

        for (var i = 0; i < invalidCandidates.Length; i++)
            AssertInvalid(invalidCandidates[i]);
    }

    private void AssertSelector(string candidate, string expectedSelector)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);
        Assert.Equal(expectedSelector, cssClass.EscapedSelector);
        Assert.Equal("display: flex;", cssClass.Styles);
    }

    private void AssertInvalid(string candidate)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.False(cssClass.IsValid, candidate);
        Assert.Empty(cssClass.EscapedSelector);
    }
}
