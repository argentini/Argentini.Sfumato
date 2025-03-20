// ReSharper disable ConvertToPrimaryConstructor

using Argentini.Sfumato.Entities;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class RegularExpressionsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public RegularExpressionsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
                                        <div id="test-home" class="text-[1rem] lg:text-[1.25rem] xl:text-(length:--my-text-size) bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="content-['Hello!'] [--margin-val6:_1.25rem]! block invisible top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
                                        <div class="-top-px"></div>
                                        <div class="top-1/2 antialiased"></div>
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
                                            
                                            var detailsMask = $"<span class=\"line-clamp-1 -mt-1! text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-2"">{description}</span></span>";
                                        }
                                        <p>top-999</p>
                                    </body>
                                    </html>
                                    """;
    
    #endregion

    [Fact]
    public void UtilityClassParsing()
    {
        const string nightmareClass1 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-[1rem]/6!";
        const string nightmareClass2 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-[1rem]!";
        const string nightmareClass3 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]";
        const string nightmareClass4 = "[&.my-item_active]:tabp:hover:-top-8!";
        const string nightmareClass5 = "group-has-[a]:tabp:hover:antialiased";
        const string nightmareClass6 = "tabp:has-checked:bg-indigo-50";
        const string nightmareClass7 = "hover:not-focus:bg-indigo-700";
        const string nightmareClass8 = "not-supports-[display:grid]:flex";
        const string nightmareClass9 = "tabp:group-hover:bg-white";
        const string nightmareClass10 = "group-[.is-published]:block";
        const string nightmareClass11 = "[font-weight:700]";
        const string nightmareClass12 = "dark:group-[.is-published]:[&.active]:tabp:hover:[font-weight:700]!";
        const string nightmareClass13 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-(length:--my-text-var)";
        const string nightmareClass14 = "dark:group-[.is-published]:[&.active]:tabp:hover:[color:var(--my-color-var)]";
        const string nightmareClass15 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]/[0.1]";
        const string nightmareClass16 = "dark:group-[.is-published]:[&.active]:tabp:hover:text-[length:var(--my-text-size-var)]/5";
            
        var appState = new AppState();

        var result1 = nightmareClass1.GetLikelyUtilityClass(appState);
        var result2 = nightmareClass2.GetLikelyUtilityClass(appState);
        var result3 = nightmareClass3.GetLikelyUtilityClass(appState);
        var result4 = nightmareClass4.GetLikelyUtilityClass(appState);
        var result5 = nightmareClass5.GetLikelyUtilityClass(appState);
        var result6 = nightmareClass6.GetLikelyUtilityClass(appState);
        var result7 = nightmareClass7.GetLikelyUtilityClass(appState);
        var result8 = nightmareClass8.GetLikelyUtilityClass(appState);
        var result9 = nightmareClass9.GetLikelyUtilityClass(appState);
        var result10 = nightmareClass10.GetLikelyUtilityClass(appState);
        var result11 = nightmareClass11.GetLikelyUtilityClass(appState);
        var result12 = nightmareClass12.GetLikelyUtilityClass(appState);
        var result13 = nightmareClass13.GetLikelyUtilityClass(appState);
        var result14 = nightmareClass14.GetLikelyUtilityClass(appState);
        var result15 = nightmareClass15.GetLikelyUtilityClass(appState);
        var result16 = nightmareClass16.GetLikelyUtilityClass(appState);

        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        Assert.NotNull(result4);
        Assert.NotNull(result5);
        Assert.NotNull(result6);
        Assert.NotNull(result7);
        Assert.NotNull(result8);
        Assert.NotNull(result9);
        Assert.NotNull(result10);
        Assert.NotNull(result11);
        Assert.NotNull(result12);
        Assert.NotNull(result13);
        Assert.NotNull(result14);
        Assert.NotNull(result15);
        Assert.NotNull(result16);
    }
    
    [Fact]
    public void FileContentParsing()
    {
        var appState = new AppState();
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, appState);

        Assert.Equal(29, utilityClasses.Count);

        _testOutputHelper.WriteLine("FileContentParsing() => Found:");
        _testOutputHelper.WriteLine("");

        foreach (var cname in utilityClasses)
            _testOutputHelper.WriteLine($"{cname.Key}");
    }
}
