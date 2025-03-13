using System.Diagnostics;
using Argentini.Sfumato.Entities;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class RegularExpressionsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    // ReSharper disable once ConvertToPrimaryConstructor
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
                                        <div id="test-home" class="text-[1rem] lg:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="content-['Hello!'] block invisible top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
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
                                            
                                            var detailsMask = $"<span class=\"line-clamp-1 mt-1 text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-2"">{description}</span></span>";
                                        }
                                    </body>
                                    </html>
                                    """;
    
    #endregion

    [Fact]
    public void TestMarkupRegex()
    {
        /*
        const string input = """
                             <div class="bg-red-500 hover:bg-blue-500 !m-4 -mt-2" data='grid-cols-[auto,1fr]'>
                             <p class='text-lg md:text-2xl font-bold'></p>
                             <span class=`text-base/5 sm:mx-2 lg:hover:opacity-75 content-['Hello!']`></span>
                             <button data-class="sm:w-1/2 !p-3">Click</button>
                             """;
        */

        var timer = new Stopwatch();
        var constants = new Library();
        var utilityClasses = new HashSet<string>();
        
        timer.Start();
        
        var quotedSubstrings = FileScanner.ScanForQuotedStrings(Markup);
        
        foreach (var quotedSubstring in quotedSubstrings)
        {
            _testOutputHelper.WriteLine($"Found quoted block => \"{quotedSubstring}\"");

            var localClasses = FileScanner.ScanStringForClasses(quotedSubstring, constants);

            foreach (var cm in localClasses)
                _testOutputHelper.WriteLine($"   Tailwind class: {cm}");

            utilityClasses.UnionWith(localClasses);
        }

        _testOutputHelper.WriteLine($"TIME: {timer.Elapsed.TotalMilliseconds} ms");

        Assert.Equal(20, quotedSubstrings.Count);
        Assert.Equal(19, utilityClasses.Count);

        utilityClasses.Clear();

        _testOutputHelper.WriteLine(""); 
        _testOutputHelper.WriteLine("Running as batch...");

        timer.Reset();
        timer.Start();
        
        utilityClasses = FileScanner.ScanFileForClasses(Markup);

        _testOutputHelper.WriteLine($"TIME: {timer.Elapsed.TotalMilliseconds} ms");

        Assert.Equal(19, utilityClasses.Count);

        _testOutputHelper.WriteLine(""); 
        _testOutputHelper.WriteLine("FINAL LIST");
        _testOutputHelper.WriteLine("");

        foreach (var cname in utilityClasses)
            _testOutputHelper.WriteLine($"{cname}");
    }
}
