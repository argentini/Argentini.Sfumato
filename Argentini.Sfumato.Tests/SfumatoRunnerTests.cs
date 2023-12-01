using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;
using Microsoft.Extensions.ObjectPool;

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
        
        Assert.False(SfumatoRunner.IsPseudoclassPrefix("sm", appState));
        Assert.False(SfumatoRunner.IsPseudoclassPrefix("dark", appState));
        Assert.True(SfumatoRunner.IsPseudoclassPrefix("hover", appState));
        Assert.True(SfumatoRunner.IsPseudoclassPrefix("focus", appState));
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Base()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var cssSelector = new CssSelector(appState, "text-base");
        await cssSelector.ProcessSelectorAsync();

        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(cssSelector, pool, string.Empty);

        Assert.Equal($$""".text-base { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.TextSizeLeadingOptions["base"]}}; }""".CompactCss(), result.CompactCss());
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Options()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var cssSelector = new CssSelector(appState, "text-base/5");
        await cssSelector.ProcessSelectorAsync();
        
        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(cssSelector, pool, string.Empty);
        
        Assert.Equal(
            $$""".text-base\/5 { font-size: {{appState.TextSizeOptions["base"]}}; line-height: {{appState.LeadingOptions["5"]}}; }""".CompactCss(), result.CompactCss());
        
        cssSelector = new CssSelector(appState, "text-base/[3rem]");
        await cssSelector.ProcessSelectorAsync();

        result = await SfumatoRunner.GenerateScssClassMarkupAsync(cssSelector, pool, string.Empty);

        Assert.Equal(
            $$""".text-base\/\[3rem\] { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 3rem; }""".CompactCss(), result.CompactCss());

        cssSelector = new CssSelector(appState, "sm:text-base/[3rem]");
        await cssSelector.ProcessSelectorAsync();

        result = await SfumatoRunner.GenerateScssClassMarkupAsync(cssSelector, pool, "sm:");
        
        Assert.Equal(
            $$""".sm\:text-base\/\[3rem\] { font-size: {{appState.TextSizeOptions["base"]}}; line-height: 3rem; }""".CompactCss(), result.CompactCss());
    }
    
    [Fact]
    public async Task GenerateScssClassMarkup_ArbitraryStyles()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());
        
        var scssClass = new CssSelector(appState, "[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();
        
        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, string.Empty);

        Assert.Equal(".\\[width\\:10rem\\] { width:10rem; }", result.CompactCss());
        
        scssClass = new CssSelector(appState, "sm:[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, "sm:");

        Assert.Equal(".sm\\:\\[width\\:10rem\\] { width:10rem; }", result.CompactCss());
        
        scssClass = new CssSelector(appState, "sm:hover:[width:10rem]", true);
        await scssClass.ProcessSelectorAsync();
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, "sm:");

        Assert.Equal(".sm\\:hover\\:\\[width\\:10rem\\] { &:hover { width:10rem; } }", result.CompactCss());
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

        var scss = await runner.GenerateUtilityScssAsync();

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

                         @include sf-media($from: sm) {
                             max-width: $sm-breakpoint;
                         }

                         @include sf-media($from: md) {
                             max-width: $md-breakpoint;
                         }

                         @include sf-media($from: lg) {
                             max-width: $lg-breakpoint;
                         }

                         @include sf-media($from: xl) {
                             max-width: $xl-breakpoint;
                         }

                         @include sf-media($from: xxl) {
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
                     @include sf-media($from: $sm-breakpoint) {
                         .sm\:\[font-weight\:900\] {
                             font-weight:900;
                         }
                     }
                     @include sf-media($from: $md-breakpoint) {
                         .md\:\[font-weight\:700\] {
                             font-weight:700;
                         }
                     }
                     @include sf-media($from: $lg-breakpoint) {
                         .lg\:\[font-weight\:600\] {
                             font-weight:600;
                         }
                         .lg\:text-\[1\.25rem\] {
                             font-size: 1.25rem;
                         }
                     }
                     @include sf-media($from: $xl-breakpoint) {
                         .xl\:text-\[\#112233\] {
                             color: #112233;
                         }
                         .xl\:text-\[color\:--my-color-var\] {
                             color: var(--my-color-var);
                         }
                         .xl\:text-\[color\:var\(--my-color-var\)\] {
                             color: var(--my-color-var);
                         }
                         .xl\:text-\[red\] {
                             color: red;
                         }
                         .xl\:text-base\/\[3rem\] {
                             font-size: {{runner.AppState.TextSizeOptions["base"]}};
                             line-height: 3rem;
                         }
                         .xl\:text\[\#112233\] {
                             color: #112233;
                         }
                     }
                     @include sf-media($from: $xxl-breakpoint) {
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

        css = css.Contains("/*") == false
            ? css
            : css[..css.IndexOf("/*", StringComparison.Ordinal)];
        
        Assert.Equal($$"""
                     @media screen and (min-width: 40em) {
                       h1 {
                         font-size: clamp(1.3125rem, 6.525vw, 1.5rem);
                         line-height: 1.75;
                         font-weight: 700 !important;
                         color: black;
                       }
                     }
                     """.CompactCss(), css.CompactCss());
    }
}
