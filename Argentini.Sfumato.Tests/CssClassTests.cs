// ReSharper disable ConvertToPrimaryConstructor

using Argentini.Sfumato.Entities.CssClassProcessing;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class CssClassTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private AppState AppState { get; } = new();

    public CssClassTests(ITestOutputHelper testOutputHelper)
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
    public void BasicUtilityClassParsing()
    {
        string[] nightmareClasses =
        [
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-[1rem]/6!",
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-[1rem]!",
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]",
            "[&.my-item_active]:tabp:hover:-top-8!",
            "group-has-[a]:tabp:hover:antialiased",
            "tabp:has-checked:bg-indigo-50",
            "hover:not-focus:bg-indigo-700",
            "not-supports-[display:grid]:flex",
            "tabp:group-hover:bg-white",
            "group-[.is-published]:block",
            "[font-weight:700]",
            "dark:group-[.is-published]:[&.active]:tabp:hover:[font-weight:700]!",
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-(length:--my-text-var)",
            "dark:group-[.is-published]:[&.active]:tabp:hover:[color:var(--my-color-var)]",
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]/[0.1]",
            "dark:group-[.is-published]:[&.active]:tabp:hover:text-[length:var(--my-text-size-var)]/5",
        ];

        foreach (var nightmareClass in nightmareClasses)
        {
            var result = new CssClass(AppState, nightmareClass);

            Assert.NotNull(result);

            _testOutputHelper.WriteLine($"{nightmareClass} => {result.IsValid}");

            Assert.True(result.IsValid);

            if (nightmareClass.EndsWith('!'))
                Assert.True(result.IsImportant);
        }
    }

    [Fact]
    public void UtilityClassProcessing()
    {
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "dark:group-[.is-published]:[&.active]:tabp:max-desk:hover:text-[1rem]/6!",
                EscapedClassName = ".group.is-published dark\\:group-\\[\\.is-published\\]\\:\\[\\&\\.active\\]\\:tabp\\:max-desk\\:hover\\:text-\\[1rem\\]\\/6\\!.active:hover",
                Styles =
                    """
                    font-size: 1rem !important;
                    line-height: calc(var(--spacing) * 6) !important;
                    """,
                IsValid = true,
                IsImportant = true,
                Wrappers =
                [
                    $"@media {AppState.Library.MediaQueryPrefixes["dark"].Statement} {{",
                    $"@media {AppState.Library.MediaQueryPrefixes["tabp"].Statement} and {AppState.Library.MediaQueryPrefixes["max-desk"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "tabp:text-indigo-400",
                EscapedClassName = ".tabp\\:text-indigo-400",
                Styles =
                    $"""
                    color: {AppState.Library.ColorsByName["indigo-400"]};
                    """,
                IsValid = true,
                Wrappers =
                [
                    $"@media {AppState.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "leading-none",
                EscapedClassName = ".leading-none",
                IsValid = true,
                Styles =
                    """
                    line-height: 1;
                    """
            },
            new ()
            {
                ClassName = "leading-2",
                EscapedClassName = ".leading-2",
                IsValid = true,
                Styles =
                    """
                    line-height: calc(var(--spacing) * 2);
                    """
            },
            new ()
            {
                ClassName = "text-indigo-400/37",
                EscapedClassName = ".text-indigo-400\\/37",
                IsValid = true,
                Styles =
                    """
                    color: oklch(0.673 0.182 276.935 / 0.37);
                    """
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppState, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.Wrappers.Length, cssClass.Wrappers.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.Wrappers.Length; i++)
            {
                Assert.Equal(test.Wrappers.ElementAt(i), cssClass.Wrappers.ElementAt(i));
            }
            
        }
    }
    
    [Fact]
    public void FileContentParsing()
    {
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, AppState);

        _testOutputHelper.WriteLine("FileContentParsing() => Found:");
        _testOutputHelper.WriteLine("");

        foreach (var cname in utilityClasses)
            _testOutputHelper.WriteLine($"{cname.Key}");
        
        Assert.Equal(26, utilityClasses.Count);
    }
}
