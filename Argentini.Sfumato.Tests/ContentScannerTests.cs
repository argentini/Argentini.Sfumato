namespace Argentini.Sfumato.Tests;

public class ContentScannerTests(ITestOutputHelper testOutputHelper)
{
    #region Constants

    public static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en" class="font-sans">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="phablet:hover:text-xs theme-midnight:text-lime-950 dark:text-base/5 xl:text-base/[3rem] [-webkit-backdrop-filter:blur(1rem)]">
                                        <div id="test-home" class="text-[1rem] lg:text-[1.25rem] xl:text-(length:--my-text-size) bg-fuchsia-500 dark:sm:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="[color:#eee;font-weight:500;] content-['Hello!'] [--margin-val6:_1.25rem]! dark:sm:supports-backdrop-blur:motion-safe:block invisible lg:max-xl:top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
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
                                                    el.classList.add(`bg-emerald-800`);
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
    public void FileContentParsing()
    {
        var appRunner = new AppRunner(new AppState());
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, appRunner);

        testOutputHelper.WriteLine("FileContentParsing() => Found:");
        testOutputHelper.WriteLine("");

        foreach (var cname in utilityClasses)
            testOutputHelper.WriteLine($"{cname.Key}");
        
        Assert.Equal(31, utilityClasses.Count);
    }
}
