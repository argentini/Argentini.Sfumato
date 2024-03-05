using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class RegularExpressionsTests
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
                                       <div id="test-home" class="text-[1rem] lg:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                           <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                           <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
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
                                       @{
                                           var test1 = $""
                                               block bg-slate-400
                                           "";
                                           
                                           var detailsMask = $"<span class=\"line-clamp-1 mt-1 text-slate-500 dark:text-dark-foreground-dim line-clamp-2\">{description}</span>";
                                       }
                                   </body>
                                   </html>
                                   """;
    
    #endregion
    
    [Fact]
    public void MatchArbitraryCssRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.ArbitraryCssRegex.Matches(Markup).DistinctBy(m => m.Value).ToList();

        Assert.Equal(8, matches.Count);
        
        appState.FilterArbitraryCssMatches(matches);
        
        Assert.Equal(6, matches.Count);
    }

    [Fact]
    public async Task MatchCoreClassesRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.CoreClassRegex.Matches(Markup).DistinctBy(m => m.Value).ToList();

        Assert.Equal(41, matches.Count);
        
        await appState.InitializeAsync(Array.Empty<string>());

        appState.FilterCoreClassMatches(matches);
        
        Assert.Equal(29, matches.Count);
        
        matches = appState.CoreClassRegex.Matches("<div class=\"!px-0\"></div>").DistinctBy(m => m.Value).ToList();

        if (matches.Count > 0)
        {
            appState.FilterCoreClassMatches(matches);
			
            Assert.Single(matches);
        }
        
        matches = appState.CoreClassRegex.Matches("<div class=\"-order-1\"></div>").DistinctBy(m => m.Value).ToList();

        if (matches.Count > 0)
        {
            appState.FilterCoreClassMatches(matches);
			
            Assert.Single(matches);
        }
    }

    [Fact]
    public void ScssApplySfumatoRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.SfumatoScssApplyRegex.Matches(@"@apply sfumato-core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
@apply sfumato-core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
@apply      sfumato-core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
@apply sfumato-core ;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
/* This is a test */
@apply sfumato-core ;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
/* This is a test */
@apply sfumato-core;

h1 {
    @apply text-2xl font-bold;
}
");

        Assert.Equal(2, matches.Count);
        
        matches = appState.SfumatoScssApplyRegex.Matches(@"
/* This is a test */
@apply    sfumato-core   ;

h1 {
    @apply   text-2xl   font-bold     ;
}
");

        Assert.Equal(2, matches.Count);
    }

    [Fact]
    public async Task ExamineMarkupForUsedClasses()
    {
        var runner = new SfumatoRunner();
        var markup = string.Empty;

        await runner.InitializeAsync();

        #region slashed
        
        runner.AppState.UsedClasses.Clear();

        var watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"text-base/5\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal($$""".text-base\/5 { font-size: {{runner.AppState.TextSizeOptions["base"]}}; line-height: {{runner.AppState.LeadingOptions["5"]}}; }""".CompactCss(), markup.CompactCss());

        #endregion

        #region slashed bracketed

        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"text-base/[3rem]\"></div>" 
        };
        
        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal($$""".text-base\/\[3rem\] { font-size: {{runner.AppState.TextSizeOptions["base"]}}; line-height: 3rem; }""".CompactCss(), markup.CompactCss());
        
        #endregion
        
        #region slashed class

        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"w-1/2\"></div>" 
        };
        
        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".w-1\\/2{\nwidth:50%;\n}", markup.Trim().Replace(" ", string.Empty));
        
        #endregion
        
        #region important
        
        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"!px-0\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".\\!px-0 { padding-left: 0px !important; padding-right: 0px !important; }".CompactCss(), markup.CompactCss());

        #endregion
        
        #region important arbitrary css
        
        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"![padding:2rem]\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".\\!\\[padding\\:2rem\\] { padding:2rem !important; }".CompactCss(), markup.CompactCss());

        #endregion
        
        #region supports-backdrop-blur
        
        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"supports-backdrop-blur:bg-slate-100\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        // markup = runner.GenerateUtilityScss();
        //
        // Assert.Equal(".\\!\\[padding\\:2rem\\] { padding:2rem !important; }".CompactCss(), markup.CompactCss());

        #endregion
    }
}
