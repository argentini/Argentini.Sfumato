using Argentini.Sfumato.Collections;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato.Tests;

public class ParsingTests
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
        Assert.Single(scssClassCollection.GetAllByClassName("bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100[--my-value]"));
        Assert.Single(scssClassCollection.GetAllByClassName("break-after-auto"));
        Assert.Equal("bg-slate-100", scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100[--my-value]").First().RootClassName);
    }

    [Fact]
    public void ReorderPrefixes()
    {
        Assert.Equal("bg-slate-100", SfumatoAppState.ReOrderPrefixes("bg-slate-100"));
        Assert.Equal("bg-slate-100[--my-value]", SfumatoAppState.ReOrderPrefixes("bg-slate-100[--my-value]"));
        Assert.Equal("font-2/2", SfumatoAppState.ReOrderPrefixes("font-2/2"));
        Assert.Equal("tabp:bg-slate-100", SfumatoAppState.ReOrderPrefixes("tabp:bg-slate-100"));
        Assert.Equal("tabp:hover:bg-slate-100", SfumatoAppState.ReOrderPrefixes("tabp:hover:bg-slate-100"));
        Assert.Equal("tabp:hover:bg-slate-100", SfumatoAppState.ReOrderPrefixes("hover:tabp:bg-slate-100"));
        Assert.Equal("dark:tabp:hover:focus:bg-slate-100[--my-value]", SfumatoAppState.ReOrderPrefixes("hover:tabp:note:focus:dark:elas:bg-slate-100[--my-value]"));
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
        Assert.Equal(string.Empty, "text-base".GetUserClassValueType());
        Assert.Equal(string.Empty, "dark:tabp:[width:3rem]".GetUserClassValueType());

        Assert.Equal("length", "dark:tabp:text-base/[3rem]".GetUserClassValueType());
        Assert.Equal("length", "dark:tabp:p-[3rem]".GetUserClassValueType());

        foreach (var unit in SfumatoScss.CssUnits)
        {
            Assert.Equal("length", $"dark:tabp:p-[3{unit}]".GetUserClassValueType());
        }

        Assert.Equal("flex", "dark:tabp:p-[3fr]".GetUserClassValueType());
        Assert.Equal("percentage", "dark:tabp:p-[3%]".GetUserClassValueType());
        Assert.Equal("percentage", "dark:tabp:p-[3.5%]".GetUserClassValueType());
        Assert.Equal("integer", "dark:tabp:p-[3]".GetUserClassValueType());
        Assert.Equal("number", "dark:tabp:p-[0.5]".GetUserClassValueType());
        Assert.Equal("number", "dark:tabp:p-[3.0]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[#123]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[#123f]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[#aa1122]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[#aa1122ff]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[rgb(1,2,3)]".GetUserClassValueType());
        Assert.Equal("color", "dark:tabp:bg-[rgba(1,2,3,0.5)]".GetUserClassValueType());
        Assert.Equal("string", "dark:tabp:content['hello_world!']".GetUserClassValueType());
        Assert.Equal("url", "dark:tabp:bg-[url(http://image.src)]".GetUserClassValueType());
        Assert.Equal("url", "dark:tabp:bg-[http://sfumato.org/images/file.jpg]".GetUserClassValueType());
        Assert.Equal("url", "dark:tabp:bg-[https://sfumato.org/images/file.jpg]".GetUserClassValueType());
        Assert.Equal("url", "dark:tabp:bg-[url(/images/file.jpg)]".GetUserClassValueType());
        Assert.Equal("url", "dark:tabp:bg-[/images/file.jpg]".GetUserClassValueType());
        
        foreach (var unit in SfumatoScss.CssAngleUnits)
        {
            Assert.Equal("angle", $"dark:tabp:rotate-[1{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssTimeUnits)
        {
            Assert.Equal("time", $"dark:tabp:duration-[100{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssFrequencyUnits)
        {
            Assert.Equal("frequency", $"dark:tabp:volume-[1{unit}]".GetUserClassValueType());
        }

        foreach (var unit in SfumatoScss.CssResolutionUnits)
        {
            Assert.Equal("resolution", $"dark:tabp:width-[1024{unit}]".GetUserClassValueType());
        }
        
        Assert.Equal("ratio", "dark:tabp:width-[1/2]".GetUserClassValueType());
        Assert.Equal("ratio", "dark:tabp:width-[1_/_2]".GetUserClassValueType());
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
}
