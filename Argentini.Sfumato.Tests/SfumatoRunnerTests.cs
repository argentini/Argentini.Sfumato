using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class SfumatoRunnerTests
{
    #region Constants

    public static string Markup => """
                                   <!DOCTYPE html>
                                   <html lang="en">
                                   <head>
                                       <meta charset="UTF-8">
                                       <title>Sample Website</title>
                                       <meta name="viewport" content="width=device-width, initial-scale=1">
                                       <link rel="stylesheet" href="css/sfumato.css">
                                   </head>
                                   <body class="text-base/5 xl:text-base/[3rem]">
                                       <div id="test-home" class="text-[1rem] lg:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[color:--my-color-var] xl:text-[color:var(--my-color-var)]">
                                           <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                           <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xl:text-slate-50-[#112233]">Invalid Classes</p>
                                       </div>
                                       <div class="block invisible top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
                                       <script>
                                           function test() {
                                             let el = document.getElementById('test-element');
                                             if (el) {
                                                   el.classList.add($`
                                                       bg-emerald-900
                                                       [font-weight:700]
                                                       md:[font-weight:700]
                                                   `);
                                                   el.classList.add(`bg-emerald-950`);
                                                   el.classList.add(`[font-weight:600]`);
                                                   el.classList.add(`lg:[font-weight:600]`);
                                             }
                                           }
                                       </script>
                                   </body>
                                   </html>
                                   """;
    
    #endregion
    
    [Fact]
    public void IsMediaQueryPrefix()
    {
        var appState = new SfumatoAppState();

        Assert.True(SfumatoRunner.IsMediaQueryPrefix("sm", appState));
        Assert.True(SfumatoRunner.IsMediaQueryPrefix("dark", appState));
        Assert.False(SfumatoRunner.IsMediaQueryPrefix("hover", appState));
        Assert.False(SfumatoRunner.IsMediaQueryPrefix("focus", appState));
    }
    
    [Fact]
    public void IsPseudoclassPrefix()
    {
        var appState = new SfumatoAppState();
        
        Assert.False(SfumatoRunner.IsPseudoClassPrefix("sm", appState));
        Assert.False(SfumatoRunner.IsPseudoClassPrefix("dark", appState));
        Assert.True(SfumatoRunner.IsPseudoClassPrefix("hover", appState));
        Assert.True(SfumatoRunner.IsPseudoClassPrefix("focus", appState));
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Base()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var cssSelector = new CssSelector(appState, "text-base");
        await cssSelector.ProcessSelectorAsync();

        var result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".text-base { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.TextSizeLeadingOptions["base"]}}; }""".CompactCss(), result.GetScssMarkup().CompactCss());
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Peer()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var regex = appState.PeerVariantRegex;
        var matches = regex.Matches("md:peer-hover:text-base");

        Assert.Single(matches);
        Assert.Equal("peer-hover:", matches[0].Value);

        matches = regex.Matches("md:peer-hover/checkbox:text-base");

        Assert.Single(matches);
        Assert.Equal("peer-hover/checkbox:", matches[0].Value);

        var cssSelector = new CssSelector(appState, "peer-hover/test:text-base");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"peer\/test:hover~.peer-hover\/test\:text-base", cssSelector.EscapedSelector);

        var result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".peer\/test:hover~.peer-hover\/test\:text-base { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.TextSizeLeadingOptions["base"]}}; }""".CompactCss(), result.GetScssMarkup().CompactCss());
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Group()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var regex = appState.GroupVariantRegex;
        var ms = regex.Matches("group-hover/test:text-base");
        Assert.Single(ms);
        ms = regex.Matches("md:group-hover/test:text-base");
        Assert.Single(ms);
        ms = regex.Matches("md:group-[.selected]:text-base");
        Assert.Single(ms);
        
        var matches = regex.Matches("md:group-hover:text-base");

        Assert.Single(matches);
        Assert.Equal("md:group-hover:", matches[0].Value);

        matches = regex.Matches("md:group-hover/checkbox:text-base");

        Assert.Single(matches);
        Assert.Equal("md:group-hover/checkbox:", matches[0].Value);

        var cssSelector = new CssSelector(appState, "group-hover/test:text-base");
        await cssSelector.ProcessSelectorAsync();
        
        Assert.Equal(@"group\/test:hover .group-hover\/test\:text-base", cssSelector.EscapedSelector);

        cssSelector = new CssSelector(appState, "group-hover/test:text-base/1");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"group\/test:hover .group-hover\/test\:text-base\/1", cssSelector.EscapedSelector);

        var result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".group\/test:hover .group-hover\/test\:text-base\/1 { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 1; }""".CompactCss(), result.GetScssMarkup().CompactCss());
        
        cssSelector = new CssSelector(appState, "group:text-base");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"group\:text-base", cssSelector.EscapedSelector);
        
        cssSelector = new CssSelector(appState, "group-[.selected]:text-base");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"group.selected .group-\[\.selected\]\:text-base", cssSelector.EscapedSelector);

        result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".group.selected .group-\[\.selected\]\:text-base { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.TextSizeLeadingOptions["base"]}}; }""".CompactCss(), result.GetScssMarkup().CompactCss());
        
        cssSelector = new CssSelector(appState, "group-[.selected]:text-base/1");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"group.selected .group-\[\.selected\]\:text-base\/1", cssSelector.EscapedSelector);

        result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".group.selected .group-\[\.selected\]\:text-base\/1 { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.LeadingOptions["1"]}}; }""".CompactCss(), result.GetScssMarkup().CompactCss());
        
        cssSelector = new CssSelector(appState, "tabp:group-[.selected]:text-base/1");
        await cssSelector.ProcessSelectorAsync();

        Assert.Equal(@"group.selected .tabp\:group-\[\.selected\]\:text-base\/1", cssSelector.EscapedSelector);
        
        result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".group.selected .tabp\:group-\[\.selected\]\:text-base\/1 { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 1; }""".CompactCss(), result.GetScssMarkup().CompactCss());
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Options()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var cssSelector = new CssSelector(appState, "text-base/5");
        await cssSelector.ProcessSelectorAsync();
        
        var result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal($$""".text-base\/5 { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.LeadingOptions["5"]}}; }""".CompactCss(), result.GetScssMarkup().CompactCss());
        
        cssSelector = new CssSelector(appState, "text-base/[3rem]");
        await cssSelector.ProcessSelectorAsync();

        result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };

        Assert.Equal(
            $$""".text-base\/\[3rem\] { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 3rem; }""".CompactCss(), result.GetScssMarkup().CompactCss());

        cssSelector = new CssSelector(appState, "sm:text-base/[3rem]");
        await cssSelector.ProcessSelectorAsync();

        result = new ScssClass
        {
            Selectors = { cssSelector.EscapedSelector },
            PseudoclassSuffix = cssSelector.PseudoclassPath,
            ScssProperties = cssSelector.GetStyles(),
            CompactScssProperties = cssSelector.ScssMarkup.CompactCss()
        };
        
        Assert.Equal(
            $$""".sm\:text-base\/\[3rem\] { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 3rem; }""".CompactCss(), result.GetScssMarkup().CompactCss());
    }
    
    [Fact]
    public async Task GenerateScssClassMarkup_ArbitraryStyles()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());
        
        var scssClass = new CssSelector(appState, "[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();
        
        var result = new ScssClass
        {
            Selectors = { scssClass.EscapedSelector },
            PseudoclassSuffix = scssClass.PseudoclassPath,
            ScssProperties = scssClass.GetStyles(),
            CompactScssProperties = scssClass.ScssMarkup.CompactCss()
        };

        Assert.Equal(@".\[width\:10rem\] { width:10rem; }".CompactCss(), result.GetScssMarkup().CompactCss());
        
        scssClass = new CssSelector(appState, "sm:[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();

        result = new ScssClass
        {
            Selectors = { scssClass.EscapedSelector },
            PseudoclassSuffix = scssClass.PseudoclassPath,
            ScssProperties = scssClass.GetStyles(),
            CompactScssProperties = scssClass.ScssMarkup.CompactCss()
        };

        Assert.Equal(@".sm\:\[width\:10rem\] { width:10rem; }".CompactCss(), result.GetScssMarkup().CompactCss());
        
        scssClass = new CssSelector(appState, "sm:hover:[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();

        result = new ScssClass
        {
            Selectors = { scssClass.EscapedSelector },
            PseudoclassSuffix = scssClass.PseudoclassPath,
            ScssProperties = scssClass.GetStyles(),
            CompactScssProperties = scssClass.ScssMarkup.CompactCss()
        };

        Assert.Equal(@".sm\:hover\:\[width\:10rem\]:hover { width:10rem; }".CompactCss(), result.GetScssMarkup().CompactCss());
    }
    
    [Fact]
    public async Task ExamineMarkupForUsedClasses()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        runner.AppState.Minify = true;
        runner.AppState.Settings.DarkMode = "media";
        
        var watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = Markup
        };
        
        await runner.AppState.ProcessFileMatchesAsync(watchedFile);
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);
        
        Assert.Equal(30, runner.AppState.UsedClasses.Count);
    }

    [Fact]
    public async Task GenerateScssObjectTree()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        runner.AppState.Minify = true;
        runner.AppState.Settings.DarkMode = "media";
        
        var watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = Markup
        };
        
        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        var scss = runner.GenerateUtilityScss();

        Assert.Equal($$"""
                     .\[font-weight\:600\] {
                         font-weight:600;
                     }
                     .\[font-weight\:700\] {
                         font-weight:700;
                     }
                     .\[font-weight\:900\] {
                         font-weight:900;
                     }
                     .aspect-screen {
                         aspect-ratio: 4/3;
                     }
                     .bg-emerald-900 {
                         background-color: rgba(6,78,59,1);
                     }
                     .bg-emerald-950 {
                         background-color: rgba(2,44,34,1);
                     }
                     .bg-fuchsia-500 {
                         background-color: rgba(217,70,239,1);
                     }
                     .block {
                         display: block;
                     }
                     .break-after-auto {
                         break-after: auto;
                     }
                     .container {
                         width: 100%;
                         margin-left: auto;
                         margin-right: auto;

                         @include sf-media($from: $sm-breakpoint) {
                             max-width: $sm-breakpoint;
                         }

                         @include sf-media($from: $md-breakpoint) {
                             max-width: $md-breakpoint;
                         }

                         @include sf-media($from: $lg-breakpoint) {
                             max-width: $lg-breakpoint;
                         }

                         @include sf-media($from: $xl-breakpoint) {
                             max-width: $xl-breakpoint;
                         }

                         @include sf-media($from: $xxl-breakpoint) {
                             max-width: $xxl-breakpoint;
                         }
                     }
                     .invisible {
                         visibility: hidden;
                     }
                     .text-\[1rem\] {
                         font-size: 1rem;
                     }
                     .text-base\/5 {
                         font-size: {{runner.AppState.TextSizeOptions["base"]}};
                         line-height: {{runner.AppState.LeadingOptions["5"]}};
                     }
                     .top-8 {
                         top: 2rem;
                     }
                     @media (min-width: #{$sm-breakpoint}) {
                         .sm\:\[font-weight\:900\] {
                             font-weight:900;
                         }
                     }
                     @media (min-width: #{$md-breakpoint}) {
                         .md\:\[font-weight\:700\] {
                             font-weight:700;
                         }
                     }
                     @media (min-width: #{$lg-breakpoint}) {
                         .lg\:\[font-weight\:600\] {
                             font-weight:600;
                         }
                         .lg\:text-\[1\.25rem\] {
                             font-size: 1.25rem;
                         }
                     }
                     @media (min-width: #{$xl-breakpoint}) {
                         .xl\:text-\[\#112233\], .xl\:text\[\#112233\] {
                             color: #112233;
                         }
                         .xl\:text-\[color\:--my-color-var\], .xl\:text-\[color\:var\(--my-color-var\)\] {
                             color: var(--my-color-var);
                         }
                         .xl\:text-\[red\] {
                             color: red;
                         }
                         .xl\:text-base\/\[3rem\] {
                             font-size: {{runner.AppState.TextSizeOptions["base"]}};
                             line-height: 3rem;
                         }
                     }
                     @media (min-width: #{$xxl-breakpoint}) {
                         .xxl\:aspect-\[8\/4\] {
                             aspect-ratio: 8/4;
                         }
                     }
                     @media (prefers-color-scheme: dark) {
                         .dark\:bg-fuchsia-300 {
                             background-color: rgba(240,171,252,1);
                         }
                         .dark\:text-\[length\:1rem\] {
                             font-size: 1rem;
                         }
                     }
                     """.CompactCss(), scss.CompactCss());
    }
    
    [Fact]
    public async Task InjectCoreAndStyles()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        const string scss = """
                            @sfumato shared;

                            @include sf-media($from: sm) {
                            
                                h1 {
                                    @apply text-2xl/[1.75] !font-bold;
                                    color: black;
                                }
                            }
                            """;

        var css = await SfumatoScss.TranspileScssAsync("test.scss", scss, runner);
        var baseline =
            """
            @media screen and (min-width: 640px) {
              h1 {
                font-size: clamp((1.5rem * 0.875), (4.35vw * 1.5), 1.5rem);
                line-height: 1.75;
                font-weight: 700 !important;
                color: black;
              }
            }
            """.CompactCss().Replace("; ", ";");

        css = css.Contains("/*") == false
            ? css.CompactCss().Replace("; ", ";")
            : css[..css.IndexOf("/*", StringComparison.Ordinal)].CompactCss().Replace("; ", ";");
        
        Assert.Equal(baseline, css);
    }
}
