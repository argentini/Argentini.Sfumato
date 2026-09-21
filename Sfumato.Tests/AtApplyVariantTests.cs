namespace Sfumato.Tests;

public sealed class AtApplyVariantTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public async Task PlainUtilityRemainsAnInlineDeclaration()
    {
        var output = await ProcessAsync("flex");

        Assert.Equal(".subject {display: flex;}", output);
    }

    [Fact]
    public async Task SelectorVariantBecomesNestedCss()
    {
        var output = await ProcessAsync("focus:flex");

        Assert.Equal(".subject {&:focus {display: flex;}}", output);
    }

    [Fact]
    public async Task MixedPlainAndVariantUtilitiesAreBothApplied()
    {
        var output = await ProcessAsync("block focus:flex");

        Assert.Contains("display: block;", output, StringComparison.Ordinal);
        Assert.Contains("&:focus {display: flex;}", output, StringComparison.Ordinal);
        Assert.DoesNotContain("@apply", output, StringComparison.Ordinal);
        Assert.DoesNotContain("@variant", output, StringComparison.Ordinal);
    }

    [Fact]
    public async Task DifferentVariantsProduceIndependentRules()
    {
        var output = await ProcessAsync("focus:flex disabled:block");

        Assert.Contains("&:focus {display: flex;}", output, StringComparison.Ordinal);
        Assert.Contains("&:disabled {display: block;}", output, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SameVariantCanApplyMultipleUtilities()
    {
        var output = await ProcessAsync("focus:flex focus:block");

        Assert.Contains("&:focus {display: flex;}", output, StringComparison.Ordinal);
        Assert.Contains("&:focus {display: block;}", output, StringComparison.Ordinal);
    }

    [Fact]
    public async Task StackedVariantsProduceOneCompoundRule()
    {
        var output = await ProcessAsync("focus:disabled:flex");

        Assert.Equal(".subject {&:focus:disabled {display: flex;}}", output);
    }

    [Fact]
    public async Task MediaVariantWrapsAppliedDeclarations()
    {
        var output = await ProcessAsync("dark:flex");

        Assert.Equal(".subject {@media (prefers-color-scheme: dark) {display: flex;}}", output);
    }

    [Fact]
    public async Task HoverVariantIncludesItsCapabilityQuery()
    {
        var output = await ProcessAsync("hover:flex");

        Assert.Equal(".subject {@media (hover: hover) {&:hover {display: flex;}}}", output);
    }

    [Fact]
    public async Task SelectorAndMediaVariantsCanBeStacked()
    {
        var output = await ProcessAsync("dark:focus:flex");

        Assert.Equal(".subject {@media (prefers-color-scheme: dark) {&:focus {display: flex;}}}", output);
    }

    [Fact]
    public async Task ArbitraryVariantAppliesItsSelector()
    {
        var output = await ProcessAsync("[&.active]:flex");

        Assert.Equal(".subject {&.active {display: flex;}}", output);
    }

    [Fact]
    public async Task DynamicGroupVariantAppliesItsAncestorSelector()
    {
        var output = await ProcessAsync("group-focus:flex");

        Assert.Equal(".subject {.group:focus & {display: flex;}}", output);
    }

    [Fact]
    public async Task ChildVariantAppliesItsRelativeSelector()
    {
        var output = await ProcessAsync("*:flex");

        Assert.Equal(".subject {:is(& > *) {display: flex;}}", output);
    }

    [Fact]
    public async Task ArbitrarySupportsVariantWrapsAppliedDeclarations()
    {
        var output = await ProcessAsync("supports-[display:grid]:flex");

        Assert.Equal(".subject {@supports (display:grid) {display: flex;}}", output);
    }

    [Fact]
    public async Task CustomVariantAppliesItsConfiguredSelector()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                }

                @custom-variant phablet (@media (width >= 475px));
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));

        var output = await ProcessAsync("phablet:flex");

        Assert.Equal(".subject {@media (width >= 475px) {display: flex;}}", output);
    }

    [Fact]
    public async Task MultilineApplyAcceptsMixedVariantSyntax()
    {
        var output = await ProcessAsync("\nblock\nfocus:flex\ndark:block\n");

        Assert.Contains("display: block;", output, StringComparison.Ordinal);
        Assert.Contains("&:focus {display: flex;}", output, StringComparison.Ordinal);
        Assert.Contains("@media (prefers-color-scheme: dark) {display: block;}", output, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ImportantVariantUtilityPreservesImportantModifier()
    {
        var output = await ProcessAsync("focus:flex\\!");

        Assert.Equal(".subject {&:focus {display: flex !important;}}", output);
    }

    private async Task<string> ProcessAsync(string utilities)
    {
        var segment = new GenerationSegment
        {
            Content = new StringBuilder($".subject {{@apply {utilities};}}")
        };

        await AppRunner.ProcessSegmentAtApplyStatementsAsync(segment);
        await AppRunner.ProcessSegmentAtVariantStatementsAsync(segment);

        return segment.Content.ToString().Replace("\r", string.Empty, StringComparison.Ordinal).Replace("\n", string.Empty, StringComparison.Ordinal);
    }
}
