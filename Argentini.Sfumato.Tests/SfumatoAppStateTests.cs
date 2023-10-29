using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class SfumatoAppStateTests
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
                                   <body class="text-base/5 desk:text-base/[3rem]">
                                       <div id="test-home" class="text-[1rem] note:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] desk:text-[#112233] desk:text-[red] desk:text-[--my-color-var] desk:text-[var(--my-color-var)]">
                                           <p class="[font-weight:900] tabp:[font-weight:900]">Placeholder</p>
                                           <p class="[fontweight:400] tabp:[fontweight:300] desk:text[#112233] desk:text-slate[#112233] desk:text-slate-50[#112233] desk:text-slate-50-[#112233]">Invalid Classes</p>
                                       </div>
                                       <div class="block invisible top-8 break-after-auto container aspect-screen elas:aspect-[8/4]"></div>
                                       <script>
                                           function test() {
                                             let el = document.getElementById('test-element');
                                             if (el) {
                                                   el.classList.add($`
                                                       bg-emerald-900
                                                       [font-weight:700]
                                                       tabl:[font-weight:700]
                                                   `);
                                                   el.classList.add(`bg-emerald-950`);
                                                   el.classList.add(`[font-weight:600]`);
                                                   el.classList.add(`note:[font-weight:600]`);
                                             }
                                           }
                                       </script>
                                   </body>
                                   </html>
                                   """;
    
    #endregion
    
    [Fact]
    public void MatchArbitraryStylesRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.ArbitraryCssRegex.Matches(Markup);

        Assert.Equal(6, matches.Count);
    }

    [Fact]
    public void MatchCoreClassesRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.CoreClassRegex.Matches(Markup);

        Assert.Equal(34, matches.Count);
    }

    [Fact]
    public void ScssIncludeSfumatoRegex()
    {
        var appState = new SfumatoAppState();
        var matches = appState.SfumatoScssIncludesRegex.Matches(@"@sfumato core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssIncludesRegex.Matches(@"
@sfumato core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssIncludesRegex.Matches(@"
@sfumato     core;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssIncludesRegex.Matches(@"
@sfumato     core ;
");

        Assert.Single(matches);
        
        matches = appState.SfumatoScssIncludesRegex.Matches(@"
/* This is a test */
@sfumato     core ;
");

        Assert.Empty(matches);
    }

    [Fact]
    public async Task LoadUtilityClasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        Assert.True(appState.UtilityClassCollection.Count > 0);
    }

    [Fact]
    public async Task GetAllByClassName()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());

        var selector = new CssSelector(appState, "bg-slate-100");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);

        selector = new CssSelector(appState, "dark:tabp:hover:bg-slate-100");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);
        
        selector = new CssSelector(appState, "dark:tabp:hover:bg-[color:--my-value]");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);
        
        selector = new CssSelector(appState, "break-after-auto");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);
        
        selector = new CssSelector(appState, "dark:tabp:hover:bg-[color:--my-value]");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);
        
        selector = new CssSelector(appState, "text-base/5");
        await selector.ProcessSelector();
        Assert.NotNull(selector.ScssUtilityClassGroup);
    }

    [Fact]
    public void GetUserClassValueType()
    {
        var appState = new SfumatoAppState();

        var package = CssSelector.SetCustomValueType("", appState);
        Assert.Equal(string.Empty, package.ValueType);
        
        package = CssSelector.SetCustomValueType("[width:3rem]", appState);
        Assert.Equal(string.Empty, package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3rem]", appState);
        Assert.Equal("length", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3fr]", appState);
        Assert.Equal("flex", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3%]", appState);
        Assert.Equal("percentage", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3.5%]", appState);
        Assert.Equal("percentage", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3]", appState);
        Assert.Equal("integer", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[0.5]", appState);
        Assert.Equal("number", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[3.0]", appState);
        Assert.Equal("number", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[#123]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[#123f]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[#aa1122]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[#aa1122ff]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[rgb(1,2,3)]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[rgba(1,2,3,0.5)]", appState);
        Assert.Equal("color", package.ValueType);
        
        package = CssSelector.SetCustomValueType("['hello_world!']", appState);
        Assert.Equal("string", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[url(http://image.src)]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[http://sfumato.org/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[https://sfumato.org/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[url(/images/file.jpg)]", appState);
        Assert.Equal("url", package.ValueType);
        
        package = CssSelector.SetCustomValueType("[/images/file.jpg]", appState);
        Assert.Equal("url", package.ValueType);
        
        foreach (var unit in appState.CssAngleUnits)
        {
            package = CssSelector.SetCustomValueType($"[1{unit}]", appState);
            Assert.Equal("angle", package.ValueType);
        }

        foreach (var unit in appState.CssTimeUnits)
        {
            package = CssSelector.SetCustomValueType($"[100{unit}]", appState);
            Assert.Equal("time", package.ValueType);
        }

        foreach (var unit in appState.CssFrequencyUnits)
        {
            package = CssSelector.SetCustomValueType($"[1{unit}]", appState);
            Assert.Equal("frequency", package.ValueType);
        }

        foreach (var unit in appState.CssResolutionUnits)
        {
            package = CssSelector.SetCustomValueType($"[1024{unit}]", appState);
            Assert.Equal("resolution", package.ValueType);
        }
        
        package = CssSelector.SetCustomValueType("[1/2]", appState);
        Assert.Equal("ratio", package.ValueType);

        package = CssSelector.SetCustomValueType("[1_/_2]", appState);
        Assert.Equal("ratio", package.ValueType);
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

        markup = await runner.GenerateScssObjectTreeAsync();
        
        Assert.Equal(".text-base\\/5 {\nfont-size: 1rem; line-height: 1.25rem;\n}".CompactCss(), markup.CompactCss());

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

        markup = await runner.GenerateScssObjectTreeAsync();
        
        Assert.Equal(".text-base\\/\\[3rem\\] { font-size: 1rem; line-height: 3rem; }".CompactCss(), markup.CompactCss());
        
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

        markup = await runner.GenerateScssObjectTreeAsync();
        
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

        markup = await runner.GenerateScssObjectTreeAsync();
        
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

        markup = await runner.GenerateScssObjectTreeAsync();
        
        Assert.Equal(".\\!\\[padding\\:2rem\\] { padding:2rem !important; }".CompactCss(), markup.CompactCss());

        #endregion
    }
}
