using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class RegularExpressionsTests
{
    #region Constants

    private static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="text-base/5 xl:text-base/[3rem] [-webkit-backdrop-filter:blur(1rem)]">
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

        Assert.Equal(9, matches.Count);
        
        appState.FilterArbitraryCssMatches(matches);
        
        Assert.Equal(7, matches.Count);
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
        else
        {
            Assert.Fail();
        }
        
        matches = appState.CoreClassRegex.Matches("<div class=\"-order-1\"></div>").DistinctBy(m => m.Value).ToList();

        if (matches.Count > 0)
        {
            appState.FilterCoreClassMatches(matches);
			
            Assert.Single(matches);
            Assert.Equal("-order-1", matches[0].Value);
        }
        else
        {
            Assert.Fail();
        }
        
        matches = appState.CoreClassRegex.Matches("$\"\"\"<div class=\"space-y-6\"></div>\"\"\"").DistinctBy(m => m.Value).ToList();

        if (matches.Count > 0)
        {
            appState.FilterCoreClassMatches(matches);
			
            Assert.Single(matches);
            Assert.Equal("space-y-6", matches[0].Value);
        }
        else
        {
            Assert.Fail();
        }
        
        matches = appState.CoreClassRegex.Matches("$\"\"\"<div class=\"text-[0.75rem]/1\"></div>\"\"\"").DistinctBy(m => m.Value).ToList();

        if (matches.Count > 0)
        {
            appState.FilterCoreClassMatches(matches);
			
            Assert.Single(matches);
            Assert.Equal("text-[0.75rem]/1", matches[0].Value);
        }
        else
        {
            Assert.Fail();
        }
        
        matches = appState.CoreClassRegex.Matches("<div class=\"border-b-canvas-tint-light-border dark:border-b-dark-canvas-tint-light-border\"></div>").DistinctBy(m => m.Value).ToList();

        Assert.Equal(2, matches.Count);

        if (matches.Count > 0)
        {
            Assert.Equal("border-b-canvas-tint-light-border", matches[0].Value);
            Assert.Equal("dark:border-b-dark-canvas-tint-light-border", matches[1].Value);
        }
        else
        {
            Assert.Fail();
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
    public void ScssSfumatoRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;
");

        Assert.Single(matches);
        Assert.Equal("@sfumato base;", matches[0].Value);
        
        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;
@sfumato utilities;
");
        
        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);

        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;@sfumato utilities;
");
        
        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);

        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;@sfumato utilities;");
        
        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);

        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base; @sfumato utilities;
");
        
        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);

        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;
@sfumato utilities;");

        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);

        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;
@sfumato utilities;
.test { }");

        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);
        
        matches = appState.SfumatoScssRegex.Matches(@"@sfumato base;
@sfumato utilities;.test { }");

        Assert.Equal(2, matches.Count);
        Assert.Equal("@sfumato base;", matches[0].Value);
        Assert.Equal("@sfumato utilities;", matches[1].Value);
    }

    [Fact]
    public void ScssValueSfumatoRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.SfumatoScssValueRegex.Matches(@"@apply sfumato-core;

    .sample {
        background-color: var(bg-primary/10);
        color:var(text-primary/10);
        padding: var(--p-5);
    }
");

        Assert.Equal(2, matches.Count);
        Assert.Equal("var(bg-primary/10)", matches[0].Value);
        Assert.Equal("var(text-primary/10)", matches[1].Value);
    }

    [Fact]
    public async Task ExamineMarkupForUsedClasses()
    {
        var runner = new SfumatoRunner();

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

        var markup = runner.GenerateUtilityScss();
        
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

        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"tabp:!px-0\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal("@media screen and (min-aspect-ratio:0.625) { .tabp\\:\\!px-0 { padding-left: 0px !important; padding-right: 0px !important; } }".CompactCss(), markup.CompactCss());
        
        #endregion

        #region negative
        
        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"-px-4 md:-px-6\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Equal(2, runner.AppState.UsedClasses.Count);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".-px-4 { padding-left: -1rem; padding-right: -1rem; } @media (min-width: #{$md-breakpoint}) { .md\\:-px-6 { padding-left: -1.5rem; padding-right: -1.5rem; } }".CompactCss(), markup.CompactCss());

        #endregion
        
        #region combined negative and important
        
        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"!-px-4 tabp:!-px-4\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Equal(2, runner.AppState.UsedClasses.Count);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".\\!-px-4 { padding-left: -1rem !important; padding-right: -1rem !important; } @media screen and (min-aspect-ratio:0.625) { .tabp\\:\\!-px-4 { padding-left: -1rem !important; padding-right: -1rem !important; } }".CompactCss(), markup.CompactCss());
        
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

        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"[-webkit-backdrop-filter:blur(1rem)]\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        
        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        markup = runner.GenerateUtilityScss();
        
        Assert.Equal(".\\[-webkit-backdrop-filter\\:blur\\(1rem\\)\\] { -webkit-backdrop-filter:blur(1rem); }".CompactCss(), markup.CompactCss());
        
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

        runner.AppState.UsedClasses.Clear();

        watchedFile = new WatchedFile
        {
            FilePath = "test.html",
            Markup = "<div class=\"group-hover/test:!block\"></div>"
        };

        await runner.AppState.ProcessFileMatchesAsync(watchedFile);        

        Assert.Single(watchedFile.CoreClassMatches);

        await runner.AppState.ExamineMarkupForUsedClassesAsync(watchedFile);

        Assert.Single(runner.AppState.UsedClasses);

        #endregion
    }
}
