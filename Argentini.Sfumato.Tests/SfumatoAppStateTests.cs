using Argentini.Sfumato.Collections;
using Argentini.Sfumato.Entities;
using Microsoft.Extensions.ObjectPool;

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

        Assert.Equal(31, matches.Count);
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
    public void GetAllByClassName()
    {
        var scssClassCollection = new ScssClassCollection();

        Assert.True(scssClassCollection.AllClasses.Count > 0);

        var selector = new CssSelector("bg-slate-100");
        Assert.Single(scssClassCollection.GetAllByClassName(selector));

        selector = new CssSelector("dark:tabp:hover:bg-slate-100");
        Assert.Single(scssClassCollection.GetAllByClassName(selector));
        
        selector = new CssSelector("dark:tabp:hover:bg-slate-100[--my-value]");
        Assert.Single(scssClassCollection.GetAllByClassName(selector));
        
        selector = new CssSelector("break-after-auto");
        Assert.Single(scssClassCollection.GetAllByClassName(selector));
        
        selector = new CssSelector("dark:tabp:hover:bg-slate-100[--my-value]");
        Assert.Equal("bg-slate-100", scssClassCollection.GetAllByClassName(selector).First().CssSelector?.RootSegment);
        
        selector = new CssSelector("text-base/5");
        Assert.Single(scssClassCollection.GetAllByClassName(selector));
    }

    [Fact]
    public void EscapeCssClassName()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        
        Assert.Equal("text-base", "text-base".EscapeCssClassName(pool));
        Assert.Equal(@"dark\:tabp\:text-base\/5", "dark:tabp:text-base/5".EscapeCssClassName(pool));
        Assert.Equal(@"dark\:tabp\:text-base\/\[3rem\]", "dark:tabp:text-base/[3rem]".EscapeCssClassName(pool));
    }

    [Fact]
    public void GetUserClassValueType()
    {
        Assert.Equal(string.Empty, "".GetUserClassValueType());
        Assert.Equal(string.Empty, "[width:3rem]".GetUserClassValueType());
        Assert.Equal("length", "[3rem]".GetUserClassValueType());
        Assert.Equal("flex", "[3fr]".GetUserClassValueType());
        Assert.Equal("percentage", "[3%]".GetUserClassValueType());
        Assert.Equal("percentage", "[3.5%]".GetUserClassValueType());
        Assert.Equal("integer", "[3]".GetUserClassValueType());
        Assert.Equal("number", "[0.5]".GetUserClassValueType());
        Assert.Equal("number", "[3.0]".GetUserClassValueType());
        Assert.Equal("color", "[#123]".GetUserClassValueType());
        Assert.Equal("color", "[#123f]".GetUserClassValueType());
        Assert.Equal("color", "[#aa1122]".GetUserClassValueType());
        Assert.Equal("color", "[#aa1122ff]".GetUserClassValueType());
        Assert.Equal("color", "[rgb(1,2,3)]".GetUserClassValueType());
        Assert.Equal("color", "[rgba(1,2,3,0.5)]".GetUserClassValueType());
        Assert.Equal("string", "['hello_world!']".GetUserClassValueType());
        Assert.Equal("url", "[url(http://image.src)]".GetUserClassValueType());
        Assert.Equal("url", "[http://sfumato.org/images/file.jpg]".GetUserClassValueType());
        Assert.Equal("url", "[https://sfumato.org/images/file.jpg]".GetUserClassValueType());
        Assert.Equal("url", "[url(/images/file.jpg)]".GetUserClassValueType());
        Assert.Equal("url", "[/images/file.jpg]".GetUserClassValueType());
        
        foreach (var unit in SfumatoScss.CssAngleUnits)
        {
            Assert.Equal("angle", $"[1{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssTimeUnits)
        {
            Assert.Equal("time", $"[100{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssFrequencyUnits)
        {
            Assert.Equal("frequency", $"[1{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssResolutionUnits)
        {
            Assert.Equal("resolution", $"[1024{unit}]".GetUserClassValueType());
        }
        
        Assert.Equal("ratio", "[1/2]".GetUserClassValueType());
        Assert.Equal("ratio", "[1_/_2]".GetUserClassValueType());
    }
    
    [Fact]
    public void GetUserClassValue()
    {
        Assert.Equal("3fr", "dark:tabp:p-[3fr]".GetUserClassValue());
        Assert.Equal("3rem", "dark:tabp:p-[3rem]".GetUserClassValue());
        Assert.Equal("#112233", "dark:tabp:p-[#112233]".GetUserClassValue());
        Assert.Equal("2rem", "dark:tabp:p-[length:2rem]".GetUserClassValue());
        Assert.Equal("width:100px", "dark:tabp:[width:100px]".GetUserClassValue());
        Assert.Equal("var(--my-width)", "dark:tabp:w-[--my-width]".GetUserClassValue());
        Assert.Equal("var(--my-width)", "dark:tabp:w-[var(--my-width)]".GetUserClassValue());
        Assert.Equal("url(\"http://sfumato.com/images/file.jpg\")", "dark:tabp:bg-[http://sfumato.com/images/file.jpg]".GetUserClassValue());
        Assert.Equal("url(\"https://sfumato.com/images/file.jpg\")", "dark:tabp:bg-[https://sfumato.com/images/file.jpg]".GetUserClassValue());
        Assert.Equal("url(\"http://sfumato.com/images/file.jpg\")", "dark:tabp:bg-[url(http://sfumato.com/images/file.jpg)]".GetUserClassValue());
    }
    
    [Fact]
    public async Task ExamineMarkupForUsedClasses()
    {
        var runner = new SfumatoRunner();
        var markup = string.Empty;

        await runner.InitializeAsync();

        #region slashed
        
        runner.AppState.UsedClasses.Clear();
        runner.AppState.ExamineMarkupForUsedClasses("<div class=\"text-base/5\"></div>");

        Assert.Single(runner.AppState.UsedClasses);

        markup = await runner.GenerateScssObjectTreeAsync();
        
        Assert.Equal(".text-base\\/5{\nfont-size:1rem;line-height:1.25rem;\n}", markup.Trim().Replace(" ", string.Empty));

        #endregion

        #region slashed bracketed

        runner.AppState.UsedClasses.Clear();
        runner.AppState.ExamineMarkupForUsedClasses("<div class=\"text-base/[3rem]\"></div>");

        Assert.Single(runner.AppState.UsedClasses);

        markup = await runner.GenerateScssObjectTreeAsync();
        
        Assert.Equal(".text-base\\/\\[3rem\\]{\nfont-size:1rem;line-height:3rem;\n}", markup.Trim().Replace(" ", string.Empty));
        
        #endregion
    }
}
