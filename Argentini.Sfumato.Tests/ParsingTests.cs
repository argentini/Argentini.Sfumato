using System.Text.RegularExpressions;
using Argentini.Sfumato.Collections;

namespace Argentini.Sfumato.Tests;

public class ParsingTests
{
    [Fact]
    public void ScssClassCollection()
    {
        var scssClassCollection = new ScssClassCollection();

        Assert.True(scssClassCollection.GetClassCount() > 0);
        Assert.Single(scssClassCollection.GetAllByClassName("bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100"));
        Assert.Single(scssClassCollection.GetAllByClassName("dark:tabp:hover:bg-slate-100[--my-value]"));
    }

    [Fact]
    public async Task UsedClasses()
    {
        var appState = new SfumatoAppState();

        await appState.InitializeAsync(Array.Empty<string>());
        await appState.GatherUsedScssCoreClassesAsync();
        
        Assert.True(appState.UsedClasses.Count > 0);

        var markup = @"
<div>
    <p class=""bg-slate-50 tabp:bg-slate-100 [font-weight:800] tabp:[font-weight:900]"">Placeholder</p>
</div>
";
        
        var matches = appState.UsedClassRegex.Matches(markup);

        Assert.Equal(4, matches.Count);
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
