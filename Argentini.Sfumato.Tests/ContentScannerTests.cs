namespace Argentini.Sfumato.Tests;

public class ContentScannerTests(ITestOutputHelper testOutputHelper)
{
    private static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en" class="nth-[3n_+_1]:text-base bg-[url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90)] [content:'arbitrary_test'] font-sans">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="@container content-['test'] content-[''] phab:hover:text-xs theme-midnight:text-lime-950 @(true ? "xl:text-base/[3rem]" : "dark:text-base/5") [-webkit-backdrop-filter:blur(1rem)]">
                                        <div id="test-home" class="@max-md:flex-col text-[1rem] lg:text-[1.25rem] xl:text-(length:--my-text-size) bg-fuchsia-500 dark:sm:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="[color:#eee;font-weight:500;] content-['Hello!'] [--margin-val6:_1.25rem]! dark:sm:supports-backdrop-blur:motion-safe:block invisible lg:max-xl:top-8 break-after-auto container aspect-screen xxl:aspect-[8/4] xl:aspect-8/4"></div>
                                        <div class="-top-px *:whitespace-pre!"></div>
                                        <div class="top-1/2 antialiased"></div>
                                        <div class="select-none">
                                            <a href="@(childUrl)" class="@(activeCategory == category.Slug ? selectedClasses : string.Empty) pl-4 -ml-px border-l"><span>@Html.Raw(category.Name.TrimStart("Subtopics:"))</span></a>
                                        </div>
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
                                            var qchar = '\"';
                                            var test1 = $""
                                                block bg-slate-400
                                            "";
                                            var array = [ "text-9xl",@"content-[\"test2\"]" ];
                                            var icon = docsNode.HasValue("iconClass") ? $"<i class=\"{docsNode.SafeValue("iconClass")} mr-2.5\"></i>" : string.Empty;
                                            var detailsMask = $"<span class=\"line-clamp-1 -mt-1! text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-3"">{description}</span></span>";
                                            
                                            return $@"
                                            <div><div class=""text-[0.65rem] mb-1.5"">{category}</div><div>{GetHeading(content)}</div></div>
                                            ";
                                        }
                                        <p>top-999</p>
                                    </body>
                                    </html>
                                    """;

    private static readonly List<string> ExpectedMatches =
    [
        "nth-[3n_+_1]:text-base",
        "bg-[url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90)]",
        "[content:'arbitrary_test']",
        "font-sans",
        "arbitrary_test",
        "viewport",
        "initial-scale=1",
        "stylesheet",
        "css/sfumato.css",
        "@container",
        "content-['test']",
        "content-['']",
        "phab:hover:text-xs",
        "theme-midnight:text-lime-950",
        "xl:text-base/[3rem]",
        "dark:text-base/5",
        "[-webkit-backdrop-filter:blur(1rem)]",
        "test",
        "test-home",
        "@max-md:flex-col",
        "text-[1rem]",
        "lg:text-[1.25rem]",
        "xl:text-(length:--my-text-size)",
        "bg-fuchsia-500",
        "dark:sm:bg-fuchsia-300",
        "dark:text-[length:1rem]",
        "xl:text-[#112233]",
        "xl:text-[red]",
        "xl:text-[--my-color-var]",
        "xl:text-[var(--my-color-var)]",
        "[font-weight:900]",
        "sm:[font-weight:900]",
        "[fontweight:400]",
        "sm:[fontweight:300]",
        "xl:text[#112233]",
        "xl:text-slate[#112233]",
        "xl:text-slate-50[#112233]",
        "xxl:text-slate-50-[#112233]",
        "[color:#eee;font-weight:500;]",
        "content-['Hello!']",
        "[--margin-val6:_1.25rem]!",
        "dark:sm:supports-backdrop-blur:motion-safe:block",
        "invisible",
        "lg:max-xl:top-8",
        "break-after-auto",
        "container",
        "aspect-screen",
        "xxl:aspect-[8/4]",
        "xl:aspect-8/4",
        "-top-px",
        "*:whitespace-pre!",
        "top-1/2",
        "antialiased",
        "select-none",
        "@(childUrl)",
        "category.Slug",
        "selectedClasses",
        "pl-4",
        "-ml-px",
        "border-l",
        "test-element",
        "bg-emerald-900",
        "[font-weight:700]",
        "md:[font-weight:700]",
        "bg-emerald-800",
        "[font-weight:600]",
        "lg:[font-weight:600]",
        "block",
        "bg-slate-400",
        "text-9xl",
        "content-[\"test2\"]",
        "test2",
        "iconClass",
        "mr-2.5",
        "line-clamp-1",
        "-mt-1!",
        "text-slate-500",
        "dark:text-dark-foreground-dim",
        "line-clamp-2",
        "line-clamp-3",
        "text-[0.65rem]",
        "mb-1.5"
    ];

    private static readonly List<string> ExpectedValidMatches =
    [
        "nth-[3n_+_1]:text-base",
        "bg-[url(/media/ze0liffq/alien-world.jpg?width=1920&quality=90)]",
        "[content:'arbitrary_test']",
        "font-sans",
        "@container",
        "content-['test']",
        "content-['']",
        "phab:hover:text-xs",
        "xl:text-base/[3rem]",
        "dark:text-base/5",
        "[-webkit-backdrop-filter:blur(1rem)]",
        "@max-md:flex-col",
        "text-[1rem]",
        "lg:text-[1.25rem]",
        "xl:text-(length:--my-text-size)",
        "bg-fuchsia-500",
        "dark:sm:bg-fuchsia-300",
        "xl:text-[#112233]",
        "xl:text-[red]",
        "xl:text-[--my-color-var]",
        "xl:text-[var(--my-color-var)]",
        "[font-weight:900]",
        "sm:[font-weight:900]",
        "[color:#eee;font-weight:500;]",
        "content-['Hello!']",
        "[--margin-val6:_1.25rem]!",
        "dark:sm:supports-backdrop-blur:motion-safe:block",
        "invisible",
        "lg:max-xl:top-8",
        "break-after-auto",
        "container",
        "aspect-screen",
        //"xxl:aspect-[8/4]",
        "xl:aspect-8/4",
        "-top-px",
        "*:whitespace-pre!",
        "top-1/2",
        "antialiased",
        "select-none",
        "pl-4",
        "-ml-px",
        "border-l",
        "bg-emerald-900",
        "[font-weight:700]",
        "md:[font-weight:700]",
        "bg-emerald-800",
        "[font-weight:600]",
        "lg:[font-weight:600]",
        "block",
        "bg-slate-400",
        "text-9xl",
        "content-[\"test2\"]",
        "mr-2.5",
        "line-clamp-1",
        "-mt-1!",
        "text-slate-500",
        "line-clamp-2",
        "line-clamp-3",
        "text-[0.65rem]",
        "mb-1.5"
    ];

    #region Constants

    private static string Css => """
                                 :root {
                                     --my-prop-var: 1.25rem;
                                     --my-prop-var2: #112233;
                                     --my-prop-var3: var(--my-prop-var4);
                                     --my-prop-var5: 1.25rem;--my-prop-var6: #112233;--my-prop-var7: var(--my-prop-var8);
                                 }

                                 .test-class {
                                     font-weight: var(--font-weight);
                                     font-size: var( --font-size );
                                     color: var (
                                         --font-color
                                         );
                                     background-color: --alpha(var(--color-lime-500) / 15%);
                                     top: --spacing(4);
                                 }
                                 """;
    #endregion

    [Fact]
    public void StringScanning()
    {
        var quotedSubstrings = new HashSet<string>(StringComparer.Ordinal);

        Markup.ScanForUtilities(quotedSubstrings);

        foreach (var substring in ExpectedMatches)
            if (quotedSubstrings.Contains(substring) == false)
                testOutputHelper.WriteLine($"NOT FOUND: `{substring}`");

        foreach (var substring in ExpectedMatches)
            if (quotedSubstrings.Contains(substring) == false)
                Assert.Fail("Did not find one or more matches");
    }
    
    [Fact]
    public void FileContentParsing()
    {
        var appRunner = new AppRunner(new AppState());
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, appRunner, true);

        testOutputHelper.WriteLine("FileContentParsing() => Found:");
        testOutputHelper.WriteLine("");

        foreach (var kvp in utilityClasses)
            testOutputHelper.WriteLine($"{kvp.Value.Selector}");

        Assert.Equal(ExpectedValidMatches.Count, utilityClasses.Count);

        foreach (var item in ExpectedValidMatches)
            if (utilityClasses.ContainsKey(item) == false)
                Assert.Fail($"NOT FOUND: `{item}`");
    }

    [Fact]
    public async Task CssCustomPropertyScanner()
    {
        var appRunner = new AppRunner(new AppState());
        var props = new List<string>();
        var segment = new GenerationSegment
        {
            Content = new StringBuilder(Css)
        };

        appRunner.GatherSegmentCssCustomPropertyRefs(segment);
        
        foreach (var kvp in segment.UsedCssCustomProperties)
        {
            testOutputHelper.WriteLine(kvp.Key);
            props.Add(kvp.Key);
        }
        
        Assert.Single(props);

        var dict = new Dictionary<string, string>();
        
        foreach (var kvp in segment.UsedCssCustomProperties)
        {
            testOutputHelper.WriteLine(kvp.Key + " : " + kvp.Value);
            dict.Add(kvp.Key, kvp.Value);
        }
        
        Assert.Single(dict);

        props.Clear();

        Assert.True(segment.Content.Contains("--alpha(var(--color-lime-500) / 15%)"));
        Assert.True(segment.Content.Contains("--spacing(4)"));

        await appRunner.ProcessSegmentFunctionsAsync(segment);

        Assert.True(segment.Content.Contains("oklch(0.768 0.233 130.85 / 0.15)"));
        Assert.True(segment.Content.Contains("calc(var(--spacing) * 4)"));
    }
}
