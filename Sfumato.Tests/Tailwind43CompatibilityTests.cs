namespace Sfumato.Tests;

public sealed class Tailwind43CompatibilityTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void LogicalSpacingUtilitiesMatchTailwind()
    {
        AssertClass("pbs-4", "padding-block-start: calc(var(--spacing) * 4);");
        AssertClass("pbe-[3px]", "padding-block-end: 3px;");
        AssertClass("mbs-auto", "margin-block-start: auto;");
        AssertClass("-mbe-2.5", "margin-block-end: calc(var(--spacing) * -2.5);");
        AssertClass("scroll-pbs-4", "scroll-padding-block-start: calc(var(--spacing) * 4);");
        AssertClass("scroll-pbe-[3px]", "scroll-padding-block-end: 3px;");
        AssertClass("scroll-mbs-4", "scroll-margin-block-start: calc(var(--spacing) * 4);");
        AssertClass("-scroll-mbe-[3px]", "scroll-margin-block-end: calc(3px * -1);");
    }

    [Fact]
    public void LogicalLayoutUtilitiesMatchTailwind()
    {
        AssertClass("inset-s-full", "inset-inline-start: 100%;");
        AssertClass("-inset-e-4", "inset-inline-end: calc(var(--spacing) * -4);");
        AssertClass("inset-bs-3/4", "inset-block-start: 75%;");
        AssertClass("inset-be-[3px]", "inset-block-end: 3px;");
        AssertClass("inline-1/2", "inline-size: 50%;");
        AssertClass("min-inline-[3px]", "min-inline-size: 3px;");
        AssertClass("max-inline-none", "max-inline-size: none;");
        AssertClass("block-lh", "block-size: 1lh;");
        AssertClass("min-block-4", "min-block-size: calc(var(--spacing) * 4);");
        AssertClass("max-block-dvh", "max-block-size: 100dvh;");
        AssertClass("border-bs", "border-block-start-style: var(--sf-border-style);\nborder-block-start-width: 1px;");
        AssertClass("border-be-4", "border-block-end-style: var(--sf-border-style);\nborder-block-end-width: 4px;");
        AssertClass("border-bs-red-500", "border-block-start-color: var(--color-red-500);");
    }

    [Fact]
    public void Tailwind43BuiltInsMatchTailwind()
    {
        AssertClass("@container-size", "container-type: size;");
        AssertClass("@container-size/sidebar", "container: sidebar / size;");
        AssertClass("@container-normal", "container-type: normal;");
        AssertClass("@container-normal/sidebar", "container: sidebar;");
        AssertClass("scrollbar-auto", "scrollbar-width: auto;");
        AssertClass("scrollbar-thin", "scrollbar-width: thin;");
        AssertClass("scrollbar-none", "scrollbar-width: none;");
        AssertClass("scrollbar-gutter-auto", "scrollbar-gutter: auto;");
        AssertClass("scrollbar-gutter-stable", "scrollbar-gutter: stable;");
        AssertClass("scrollbar-gutter-both", "scrollbar-gutter: stable both-edges;");
        AssertClass("scrollbar-thumb-red-500", "--sf-scrollbar-thumb: var(--color-red-500);\nscrollbar-color: var(--sf-scrollbar-thumb) var(--sf-scrollbar-track);");
        AssertClass("scrollbar-track-[#0088cc]", "--sf-scrollbar-track: #0088cc;\nscrollbar-color: var(--sf-scrollbar-thumb) var(--sf-scrollbar-track);");
        AssertClass("zoom-50", "zoom: 50%;");
        AssertClass("zoom-[var(--zoom)]", "zoom: var(--zoom);");
        AssertClass("tab-4", "tab-size: 4;");
        AssertClass("tab-[12px]", "tab-size: 12px;");
        AssertClass("font-features-[\"smcp\"]", "font-feature-settings: \"smcp\";");
        AssertClass("auto-cols-4", "grid-auto-columns: calc(var(--spacing) * 4);");
        AssertClass("auto-rows-2.5", "grid-auto-rows: calc(var(--spacing) * 2.5);");
    }

    [Fact]
    public void AspectRatiosUseQuarterStepBareValues()
    {
        AssertClass("aspect-8.5/11", "aspect-ratio: 8.5 / 11;");
        AssertInvalid("aspect-1.23/4.56");
    }

    [Fact]
    public void InvalidNegativeUtilitiesEmitNothing()
    {
        AssertInvalid("-p-4");
        AssertInvalid("-pbs-[3px]");
        AssertInvalid("-scroll-pbs-4");
        AssertInvalid("-inline-4");
        AssertInvalid("-zoom-50");
        AssertInvalid("-tab-4");
        AssertInvalid("-@container-size");
        AssertInvalid("zoom-1.5");
        AssertInvalid("tab-1.5");
        AssertInvalid("tab-1/2");
        AssertInvalid("tab-[12px]/foo");
        AssertInvalid("font-features-[\"smcp\"]/foo");
        AssertInvalid("min-inline-screen");
        AssertInvalid("max-inline-dvw");
        AssertInvalid("min-inline-1/2");
        AssertInvalid("max-block-1/2");
        AssertInvalid("scrollbar-auto/foo");
        AssertInvalid("scrollbar-gutter-auto/foo");
        AssertInvalid("scrollbar-thumb-red-500/foo");
        AssertInvalid("scrollbar-thumb-[#0088cc]/foo");
        AssertInvalid("shadow-sm/foo");
        AssertInvalid("inset-shadow-sm/foo");
        AssertInvalid("text-shadow-sm/foo");
        AssertInvalid("drop-shadow-sm/foo");
    }

    [Fact]
    public void NewNeutralPalettesUseFullSfumatoScale()
    {
        string[] palettes = ["mauve", "olive", "mist", "taupe"];
        int[] stops = [50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000];

        for (var paletteIndex = 0; paletteIndex < palettes.Length; paletteIndex++)
        {
            for (var stopIndex = 0; stopIndex < stops.Length; stopIndex++)
                Assert.True(AppRunner.Library.ColorsByName.ContainsKey($"{palettes[paletteIndex]}-{stops[stopIndex]}"));
        }

        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("mauve-100", out var mauve100));
        Assert.Equal("oklch(0.96 0.003 325.6)", mauve100);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("mauve-150", out var mauve150));
        Assert.Equal("oklch(0.941 0.004 325.61)", mauve150);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("olive-100", out var olive100));
        Assert.Equal("oklch(0.966 0.005 106.5)", olive100);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("olive-150", out var olive150));
        Assert.Equal("oklch(0.948 0.006 106.5)", olive150);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("mist-100", out var mist100));
        Assert.Equal("oklch(0.963 0.002 197.1)", mist100);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("mist-150", out var mist150));
        Assert.Equal("oklch(0.944 0.004 205.7)", mist150);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("taupe-150", out var taupe150));
        Assert.Equal("oklch(0.941 0.004 25.75)", taupe150);
        Assert.True(AppRunner.Library.ColorsByName.TryGetValue("taupe-950", out var taupe950));
        Assert.Equal("oklch(0.147 0.004 49.3)", taupe950);
        AssertClass("bg-mist-650", "background-color: var(--color-mist-650);");
    }

    [Fact]
    public void LogicalUtilitiesResolveNamedThemeValues()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                    --spacing-big: 100rem;
                    --inset-shadowned: 1940px;
                }
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));
        AssertClass("pbs-big", "padding-block-start: var(--spacing-big);");
        AssertClass("-mbe-big", "margin-block-end: calc(var(--spacing-big) * -1);");
        AssertClass("scroll-pbe-big", "scroll-padding-block-end: var(--spacing-big);");
        AssertClass("-scroll-mbs-big", "scroll-margin-block-start: calc(var(--spacing-big) * -1);");
        AssertClass("inset-s-shadowned", "inset-inline-start: var(--inset-shadowned);");
        AssertClass("-inset-be-shadowned", "inset-block-end: calc(var(--inset-shadowned) * -1);");
        AssertInvalid("inset-s-shadow-sm");
    }

    [Fact]
    public void PreflightMatchesTailwindPatchSemantics()
    {
        var reset = Constants.LoadBrowserResetCss();

        Assert.DoesNotContain("outline-color: transparent", reset, StringComparison.Ordinal);
        Assert.DoesNotContain("border-color: transparent", reset, StringComparison.Ordinal);
        Assert.Contains(":-moz-focusring:where(:not(iframe))", reset, StringComparison.Ordinal);
        Assert.Contains("::-webkit-calendar-picker-indicator", reset, StringComparison.Ordinal);
        Assert.Contains("BlinkMacSystemFont", reset, StringComparison.Ordinal);
        Assert.Contains("'Segoe UI'", reset, StringComparison.Ordinal);
        Assert.Contains("'Courier New'", reset, StringComparison.Ordinal);
    }

    [Fact]
    public void FractionalShadowOpacityMatchesTailwindPatch()
    {
        AssertClassContains("shadow-sm/12.5", "--sf-shadow-alpha: 12.5%;");
        AssertClassContains("inset-shadow-sm/12.5", "--sf-inset-shadow-alpha: 12.5%;");
        AssertClassContains("text-shadow-sm/12.5", "--sf-text-shadow-alpha: 12.5%;");
        AssertClassContains("drop-shadow-sm/12.5", "--sf-drop-shadow-alpha: 12.5%;");
    }

    [Fact]
    public void ArbitraryColorOpacityUsesAlphaSemantics()
    {
        AssertClassContains("scrollbar-thumb-red-500/[0.5]", "50%, transparent");
        AssertClassContains("scrollbar-track-[#0088cc]/[0.5]", "rgba(0,136,204,0.5)");
    }

    [Fact]
    public async Task SpacingFunctionsPreserveLengthSemantics()
    {
        AssertClass("m-1", "margin: var(--spacing);");

        using var fontSize = new CssClass(AppRunner, selector: "text-[--spacing(2)]");

        Assert.True(fontSize.IsValid);
        Assert.Equal("font-size: --spacing(2);", fontSize.Styles);

        var segment = new GenerationSegment
        {
            Content = new StringBuilder(".sample { margin: --spacing(0); padding: --spacing(0px); font-size: --spacing(2); }")
        };

        await AppRunner.ProcessSegmentFunctionsAsync(segment);

        Assert.Equal(".sample { margin: 0px; padding: 0px; font-size: calc(var(--spacing) * 2); }", segment.Content.ToString());
    }

    [Fact]
    public void FunctionalUtilitiesResolveDefaultsAndModifiers()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                }

                @utility example-* {
                    --resolved-value: --value(integer, --default(12));
                    --resolved-modifier: --modifier(integer, --default(34));
                }
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));
        Assert.True(
            AppRunner.Library.FunctionalClasses.ContainsKey("example"),
            string.Join(" | ", AppRunner.AppRunnerSettings.SfumatoBlockItems.Keys));
        AssertClass("example", "--resolved-value: 12;\n--resolved-modifier: 34;");
        AssertClass("example-1", "--resolved-value: 1;\n--resolved-modifier: 34;");
        AssertClass("example/2", "--resolved-value: 12;\n--resolved-modifier: 2;");
        AssertClass("example-1/2", "--resolved-value: 1;\n--resolved-modifier: 2;");
        AssertInvalid("example-foo");
    }

    [Fact]
    public void FunctionalUtilitiesSupportTypedOverloads()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                }

                @utility dual-* {
                    color: --value(--color-*);
                }

                @utility dual-* {
                    width: --value([length]);
                }

                @utility invalid-* {
                    color: red;
                }
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));
        AssertClass("dual-red-500", "color: var(--color-red-500);");
        AssertClass("dual-[3px]", "width: 3px;");
        AssertInvalid("invalid-red");
    }

    [Fact]
    public void NegatedContainerVariantsMatchTailwindPatch()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                }

                @custom-variant has-a (@container style(--a));
                @custom-variant has-b (@container not style(--b));
                @custom-variant has-c (@container card style(--c));
                @custom-variant has-d (@container card not style(--d));
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));
        AssertWrapper("not-has-a:flex", "@container not style(--a) {");
        AssertWrapper("not-has-b:flex", "@container style(--b) {");
        AssertWrapper("not-has-c:flex", "@container card not style(--c) {");
        AssertWrapper("not-has-d:flex", "@container card style(--d) {");
    }

    [Fact]
    public void CustomVariantsReceiveNegatedCompanions()
    {
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                }

                @custom-variant interactive (&:hover);
                @custom-variant motion (@media (prefers-reduced-motion: reduce));
                @custom-variant grid-supported (@supports (display: grid));
            }
            """;

        Assert.True(css.LoadSfumatoSettings(AppRunner));
        AssertSelector("not-interactive:flex", @".not-interactive\:flex:not(:hover)");
        AssertWrapper("not-motion:flex", "@media not (prefers-reduced-motion: reduce) {");
        AssertWrapper("not-grid-supported:flex", "@supports not (display: grid) {");
    }

    [Fact]
    public async Task VariantRulesSupportStackingAndCompounds()
    {
        var segment = new GenerationSegment
        {
            Content = new StringBuilder(
                """
                .button {
                    @variant hover:focus, disabled {
                        color: red;
                    }
                }
                """)
        };

        await AppRunner.ProcessSegmentAtVariantStatementsAsync(segment);

        var output = segment.Content.ToString();

        Assert.DoesNotContain("@variant", output, StringComparison.Ordinal);
        Assert.Contains("&:hover:focus", output, StringComparison.Ordinal);
        Assert.Contains("&:disabled", output, StringComparison.Ordinal);
    }

    private void AssertClass(string candidate, string expectedStyles)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);
        Assert.Equal(expectedStyles, cssClass.Styles);
    }

    private void AssertInvalid(string candidate)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.False(cssClass.IsValid, candidate);
        Assert.Empty(cssClass.Styles);
    }

    private void AssertClassContains(string candidate, string expectedStyles)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);
        Assert.Contains(expectedStyles, cssClass.Styles, StringComparison.Ordinal);
    }

    private void AssertWrapper(string candidate, string expectedWrapper)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);
        Assert.Contains(expectedWrapper, cssClass.Wrappers.Values);
    }

    private void AssertSelector(string candidate, string expectedSelector)
    {
        using var cssClass = new CssClass(AppRunner, selector: candidate);

        Assert.True(cssClass.IsValid, candidate);
        Assert.Equal(expectedSelector, cssClass.EscapedSelector);
    }
}
