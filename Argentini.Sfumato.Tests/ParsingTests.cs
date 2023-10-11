using Argentini.Sfumato.Collections;

namespace Argentini.Sfumato.Tests;

public class ParsingTests
{
    [Fact]
    public void Regex()
    {
        var appState = new SfumatoAppState();
        const string markup = """
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
        var matches = appState.ArbitraryCssRegex.Matches(markup);

        Assert.Equal(6, matches.Count);        

        matches = appState.CoreClassRegex.Matches(markup);

        Assert.Equal(29, matches.Count);
    }
    
    [Fact]
    public void ScssClassCollection()
    {
        var scssClassCollection = new ScssClassCollection();

        Assert.True(scssClassCollection.AllClasses.Count > 0);
        Assert.Single(scssClassCollection.GetAllByClassName("bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100[--my-value]"));
        Assert.Single(scssClassCollection.GetAllByClassName("break-after-auto"));
    }
    
//     [Fact]
//     public async Task GenerateColorsCode()
//     {
//         var sb = new StringBuilder();
//         var scss = await File.ReadAllTextAsync(Path.Combine("scss", "text-color.scss"));
//
//         if (string.IsNullOrEmpty(scss))
//             Assert.Fail();
//
//         var index = 0;
//
//         while (index > -1)
//         {
//             if (scss[index] != '.')
//             {
//                 index = scss.IndexOf("\n.", index, StringComparison.Ordinal);
//
//                 if (index > -1)
//                     index++;
//             }
//
//             if (index < 0)
//                 continue;
// 				
//             var end = scss.IndexOf(' ', index);
//
//             if (end < 0)
//                 continue;
//
//             var propertyPrefix = "color:";
//             var valueStartIndex = scss.IndexOf(propertyPrefix, end, StringComparison.Ordinal);
//
//             if (valueStartIndex > -1)
//             {
//                 valueStartIndex += propertyPrefix.Length;
//                 
//                 var valueEndIndex = scss.IndexOf(";", valueStartIndex, StringComparison.Ordinal);
//
//                 if (valueEndIndex > -1)
//                 {
//                     var defaultValue = scss[valueStartIndex..valueEndIndex].Trim();
//                     
//                     sb.Append(
//                         @$"[""{scss.Substring(index + 1, end - index - 1)}""] = new ScssClass
// {{
//     ValueMask = ""{{value}}"",
//     Value = ""{defaultValue}"",
//     ValueType = ""color"",
//     Template = ""background-color: {{value}};""
// }},
// ");            
//                 }
//             }
//
//             index = end;
//         }
//         
//         Assert.NotEmpty(sb.ToString());
//     }
}
