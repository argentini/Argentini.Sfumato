namespace Sfumato.Tests;

public sealed class ArbitraryContainerQueryVariantTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    private static readonly (string Prefix, string Condition)[] QueryKinds =
    [
        ("min", "width >="),
        ("max", "width <"),
    ];

    [Fact]
    public void ArbitraryContainerQueriesSupportEveryDirectSyntaxPermutation()
    {
        for (var queryIndex = 0; queryIndex < QueryKinds.Length; queryIndex++)
        {
            var (prefix, condition) = QueryKinds[queryIndex];

            for (var razorIndex = 0; razorIndex < 2; razorIndex++)
            {
                var at = razorIndex == 0 ? "@" : "@@";

                for (var namedIndex = 0; namedIndex < 2; namedIndex++)
                {
                    var name = namedIndex == 0 ? string.Empty : "/main";

                    for (var importantIndex = 0; importantIndex < 2; importantIndex++)
                    {
                        var important = importantIndex == 1 ? "!" : string.Empty;
                        var candidate = $"{at}{prefix}-[475px]{name}:flex{important}";
                        var containerName = namedIndex == 0 ? string.Empty : "main ";

                        AssertContainerVariant(candidate, $"@container {containerName}({condition} 475px) {{", importantIndex == 1);
                    }
                }
            }
        }
    }

    [Fact]
    public void ArbitraryContainerQueriesSupportEveryRangeStackingPermutation()
    {
        string[][] orderings =
        [
            ["min", "max", "hover"],
            ["min", "hover", "max"],
            ["max", "min", "hover"],
            ["max", "hover", "min"],
            ["hover", "min", "max"],
            ["hover", "max", "min"],
        ];

        for (var razorIndex = 0; razorIndex < 2; razorIndex++)
        {
            var at = razorIndex == 0 ? "@" : "@@";

            for (var namedIndex = 0; namedIndex < 2; namedIndex++)
            {
                var name = namedIndex == 0 ? string.Empty : "/main";
                var containerName = namedIndex == 0 ? string.Empty : "main ";

                for (var orderingIndex = 0; orderingIndex < orderings.Length; orderingIndex++)
                {
                    var variants = new string[orderings[orderingIndex].Length];

                    for (var variantIndex = 0; variantIndex < variants.Length; variantIndex++)
                    {
                        variants[variantIndex] = orderings[orderingIndex][variantIndex] switch
                        {
                            "min" => $"{at}min-[475px]{name}",
                            "max" => $"{at}max-[960px]{name}",
                            _ => "hover",
                        };
                    }

                    var candidate = $"{string.Join(':', variants)}:flex!";

                    AssertContainerVariant(
                        candidate,
                        $"@container {containerName}(width >= 475px) and (width < 960px) {{",
                        isImportant: true,
                        hasHover: true);
                }
            }
        }
    }

    [Fact]
    public void ArbitraryContainerQueriesStackWithMediaVariants()
    {
        using var cssClass = new CssClass(AppRunner, selector: "md:@min-[475px]:@max-[960px]:flex");

        Assert.True(cssClass.IsValid);
        Assert.Equal(@".md\:\@min-\[475px\]\:\@max-\[960px\]\:flex", cssClass.EscapedSelector);
        Assert.Equal("display: flex;", cssClass.Styles);
        Assert.Equal(2, cssClass.Wrappers.Count);
        Assert.Contains($"@media {AppRunner.Library.MediaQueryPrefixes["md"].Statement} {{", cssClass.Wrappers.Values);
        Assert.Contains("@container (width >= 475px) and (width < 960px) {", cssClass.Wrappers.Values);
    }

    [Fact]
    public void ArbitraryContainerQueriesPreserveComplexValues()
    {
        (string Candidate, string Wrapper)[] cases =
        [
            ("@min-[calc(100%_-_1rem)]:flex", "@container (width >= calc(100% - 1rem)) {"),
            ("@max-[var(--content-width)]:flex", "@container (width < var(--content-width)) {"),
            ("@min-[calc(100cqw/2)]:flex", "@container (width >= calc(100cqw/2)) {"),
            ("@max-[calc(100cqw/2)]/main:flex", "@container main (width < calc(100cqw/2)) {"),
        ];

        for (var index = 0; index < cases.Length; index++)
            AssertContainerVariant(cases[index].Candidate, cases[index].Wrapper, assertSelector: false);
    }

    [Fact]
    public void ArbitraryContainerQueriesRejectMalformedSyntax()
    {
        string[] candidates =
        [
            "@min-[]:flex",
            "@max-[]:flex",
            "@min-[475px:flex",
            "@max-475px:flex",
            "@min-(--container-width):flex",
            "@max-[960px]/:flex",
            "@min-[475px]/main/other:flex",
            "@@min-[]:flex",
            "@@max-[960px]/:flex",
        ];

        for (var index = 0; index < candidates.Length; index++)
        {
            using var cssClass = new CssClass(AppRunner, selector: candidates[index]);

            Assert.False(cssClass.IsValid, candidates[index]);
            Assert.Empty(cssClass.EscapedSelector);
            Assert.Empty(cssClass.Wrappers);
        }
    }

    private void AssertContainerVariant(string candidate, string expectedWrapper, bool isImportant = false, bool hasHover = false, bool assertSelector = true)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);

        if (assertSelector)
            Assert.Equal(EscapeSelector(candidate), cssClass.EscapedSelector);

        Assert.Equal(isImportant ? "display: flex !important;" : "display: flex;", cssClass.Styles);
        Assert.Equal(isImportant, cssClass.IsImportant);
        Assert.Equal(hasHover, cssClass.EscapedSelector.EndsWith(":hover", StringComparison.Ordinal));
        Assert.Single(cssClass.Wrappers);
        Assert.Equal(expectedWrapper, cssClass.Wrappers.First().Value);
    }

    private static string EscapeSelector(string candidate)
    {
        return "." + candidate
            .Replace("@@", "@", StringComparison.Ordinal)
            .Replace("@", @"\@", StringComparison.Ordinal)
            .Replace("[", @"\[", StringComparison.Ordinal)
            .Replace("]", @"\]", StringComparison.Ordinal)
            .Replace("/", @"\/", StringComparison.Ordinal)
            .Replace(":", @"\:", StringComparison.Ordinal)
            .Replace("!", @"\!", StringComparison.Ordinal)
            + (candidate.Contains("hover", StringComparison.Ordinal) ? ":hover" : string.Empty);
    }
}
