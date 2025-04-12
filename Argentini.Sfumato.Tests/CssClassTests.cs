// ReSharper disable ConvertToPrimaryConstructor

using System.Text;
using Argentini.Sfumato.Entities.CssClassProcessing;
using Argentini.Sfumato.Extensions;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class CssClassTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CssClassTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    #region Constants

    private static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en" class="font-sans">
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

        var appRunner = new AppRunner(new AppState());

        foreach (var nightmareClass in nightmareClasses)
        {
            var result = new CssClass(appRunner, nightmareClass);

            Assert.NotNull(result);

            _testOutputHelper.WriteLine($"{nightmareClass} => {result.IsValid}");

            Assert.True(result.IsValid);

            if (nightmareClass.EndsWith('!'))
                Assert.True(result.IsImportant);
        }
    }

    [Fact]
    public void StringCrc32()
    {
        Assert.Equal((uint)4044826889, "@media screen (min-width: 40rem) and (max-width: 80rem) {".GenerateCrc32());
        Assert.Equal((uint)1465918141, "@media screen (min-width: 40rem) and (max-width: 81rem) {".GenerateCrc32());
        Assert.Equal((uint)3657485972, "@media screen (min-width: 40rem) {".GenerateCrc32());
    }

    [Fact]
    public void StringBuilderCrc32()
    {
        Assert.Equal((uint)4044826889, new StringBuilder("@media screen (min-width: 40rem) and (max-width: 80rem) {").GenerateCrc32());
        Assert.Equal((uint)1465918141, new StringBuilder("@media screen (min-width: 40rem) and (max-width: 81rem) {").GenerateCrc32());
        Assert.Equal((uint)3657485972, new StringBuilder("@media screen (min-width: 40rem) {").GenerateCrc32());
    }

    [Fact]
    public void UtilityClassProcessing()
    {
        var appRunner = new AppRunner(new AppState());
        var testClasses = new List<TestClass>()
        {
            new ()
            {
                ClassName = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!",
                EscapedClassName = @".group.is-published .dark\:group-\[\.is-published\]\:\[\&\.active\]\:\[\@supports\(display\:flex\)\]\:tabp\:max-desk\:hover\:text-\[1rem\]\/6\!.active:hover",
                Styles =
                    """
                    font-size: 1rem !important;
                    line-height: calc(var(--spacing) * 6) !important;
                    """,
                IsValid = true,
                IsImportant = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["dark"].Statement} {{",
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} and {appRunner.Library.MediaQueryPrefixes["max-desk"].Statement} {{",
                    "@supports(display:flex) {",
                ]
            },
            new ()
            {
                ClassName = "leading-none",
                EscapedClassName = ".leading-none",
                IsValid = true,
                Styles = "line-height: 1;"
            },
            new ()
            {
                ClassName = "leading-2",
                EscapedClassName = ".leading-2",
                IsValid = true,
                Styles = "line-height: calc(var(--spacing) * 2);"
            },
            new ()
            {
                ClassName = "-leading-2",
                EscapedClassName = ".-leading-2",
                IsValid = true,
                Styles = "line-height: calc(var(--spacing) * 2 * -1);"
            },
            new ()
            {
                ClassName = "leading-[1.35]",
                EscapedClassName = @".leading-\[1\.35\]",
                IsValid = true,
                Styles = "line-height: 1.35;"
            },
            new ()
            {
                ClassName = "leading-[1.5rem]",
                EscapedClassName = @".leading-\[1\.5rem\]",
                IsValid = true,
                Styles = "line-height: 1.5rem;"
            },
            new ()
            {
                ClassName = "tabp:text-indigo-400",
                EscapedClassName = @".tabp\:text-indigo-400",
                Styles =
                    $"color: {appRunner.Library.ColorsByName["indigo-400"]};",
                IsValid = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "text-indigo-400/37",
                EscapedClassName = @".text-indigo-400\/37",
                IsValid = true,
                Styles = "color: oklch(0.673 0.182 276.935 / 0.37);"
            },
            new ()
            {
                ClassName = "tabp:text-[#ffffff]",
                EscapedClassName = @".tabp\:text-\[\#ffffff\]",
                Styles =
                    "color: #ffffff;",
                IsValid = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "tabp:text-[#ffffff]/50",
                EscapedClassName = @".tabp\:text-\[\#ffffff\]\/50",
                Styles =
                    "color: rgba(255,255,255,0.5);",
                IsValid = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "tabp:text-[red]/50",
                EscapedClassName = @".tabp\:text-\[red\]\/50",
                Styles =
                    "color: rgba(255,0,0,0.5);",
                IsValid = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "tabp:text-[red]/[0.25]",
                EscapedClassName = @".tabp\:text-\[red\]\/\[0\.25\]",
                Styles =
                    "color: rgba(255,0,0,0.25);",
                IsValid = true,
                Wrappers =
                [
                    $"@media {appRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "text-base",
                EscapedClassName = ".text-base",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--text-base--line-height);
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "text-base/5",
                EscapedClassName = @".text-base\/5",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: calc(var(--spacing) * 5);
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "text-base/[1.25rem]",
                EscapedClassName = @".text-base\/\[1\.25rem\]",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: 1.25rem;
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "text-[1.25rem]/5",
                EscapedClassName = @".text-\[1\.25rem\]\/5",
                Styles =
                    """
                    font-size: 1.25rem;
                    line-height: calc(var(--spacing) * 5);
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "text-[1.25rem]/[1.35rem]",
                EscapedClassName = @".text-\[1\.25rem\]\/\[1\.35rem\]",
                Styles =
                    """
                    font-size: 1.25rem;
                    line-height: 1.35rem;
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "bg-indigo-400",
                EscapedClassName = ".bg-indigo-400",
                Styles =
                    $"""
                    background-color: {appRunner.Library.ColorsByName["indigo-400"]};
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "bg-[url('/images/test.jpg')]",
                EscapedClassName = @".bg-\[url\(\'\/images\/test\.jpg\'\)\]",
                Styles = "background-image: url('/images/test.jpg');",
                IsValid = true
            },
            new ()
            {
                ClassName = "bg-(image:--my-custom-image)",
                EscapedClassName = @".bg-\(image\:--my-custom-image\)",
                Styles = "background-image: var(--my-custom-image);",
                IsValid = true
            },
            new ()
            {
                ClassName = "top-3",
                EscapedClassName = @".top-3",
                Styles = "top: calc(var(--spacing) * 3);",
                IsValid = true
            },
            new ()
            {
                ClassName = "-top-4",
                EscapedClassName = @".-top-4",
                Styles = "top: calc(var(--spacing) * -4);",
                IsValid = true
            },
            new ()
            {
                ClassName = "top-1/2",
                EscapedClassName = @".top-1\/2",
                Styles = "top: 50%;",
                IsValid = true
            },
            new ()
            {
                ClassName = "top-2/3",
                EscapedClassName = @".top-2\/3",
                Styles = "top: 66.666666666667%;",
                IsValid = true
            },
            new ()
            {
                ClassName = "@container",
                EscapedClassName = @".\@container",
                Styles = "container-type: inline-size;",
                IsValid = true
            },
            new ()
            {
                ClassName = "@container/primary",
                EscapedClassName = @".\@container\/primary",
                Styles =
                    """
                    container-type: inline-size;
                    container-name: primary;
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "@sm:@max-lg:leading-none",
                EscapedClassName = @".\@sm\:\@max-lg\:leading-none",
                Styles =
                    """
                    line-height: 1;
                    """,
                IsValid = true,
                Wrappers =
                [
                    $"@container {appRunner.Library.ContainerQueryPrefixes["@sm"].Statement} and {appRunner.Library.ContainerQueryPrefixes["@max-lg"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "@sm/primary:@max-lg/primary:leading-none",
                EscapedClassName = @".\@sm\/primary\:\@max-lg\/primary\:leading-none",
                Styles =
                    """
                    line-height: 1;
                    """,
                IsValid = true,
                Wrappers =
                [
                    $"@container primary {appRunner.Library.ContainerQueryPrefixes["@sm"].Statement} and {appRunner.Library.ContainerQueryPrefixes["@max-lg"].Statement} {{"
                ]
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(appRunner, test.ClassName);

            Assert.NotNull(cssClass);
            Assert.Equal(test.IsValid, cssClass.IsValid);
            Assert.Equal(test.IsImportant, cssClass.IsImportant);
            Assert.Equal(test.EscapedClassName, cssClass.EscapedSelector);
            Assert.Equal(test.Wrappers.Length, cssClass.Wrappers.Count);
            Assert.Equal(test.Styles, cssClass.Styles);

            for (var i = 0; i < test.Wrappers.Length; i++)
            {
                Assert.Equal(test.Wrappers.ElementAt(i), cssClass.Wrappers.ElementAt(i).Value);
            }
            
            _testOutputHelper.WriteLine($"UtilityClassProcessing() => {test.ClassName}");
        }
    }
    
    [Fact]
    public void FileContentParsing()
    {
        var appRunner = new AppRunner(new AppState());
        var utilityClasses = ContentScanner.ScanFileForUtilityClasses(Markup, appRunner);

        _testOutputHelper.WriteLine("FileContentParsing() => Found:");
        _testOutputHelper.WriteLine("");

        foreach (var cname in utilityClasses)
            _testOutputHelper.WriteLine($"{cname.Key}");
        
        Assert.Equal(27, utilityClasses.Count);
    }
}
