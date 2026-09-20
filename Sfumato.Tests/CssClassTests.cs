namespace Sfumato.Tests;

public class CssClassTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
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
                                        <div class="content-['Hello!'] [--margin-val6:_1.25rem]! dark:sm:supports-backdrop-blur:motion-safe:block invisible lg:max-xl:top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
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
            var result = new CssClass(AppRunner, selector: nightmareClass);

            Assert.NotNull(result);

            TestOutputHelper?.WriteLine($"{nightmareClass} => {result.IsValid}");

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
                ClassName = "top-[-0.65rem]",
                EscapedClassName = @".top-\[-0\.65rem\]",
                Styles =
                    """
                    top: -0.65rem;
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "hover:text-base",
                EscapedClassName = @".hover\:text-base:hover",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--sf-leading, var(--text-base--line-height));
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "nth-3:text-base",
                EscapedClassName = @".nth-3\:text-base:nth-child(3), .nth-3\:text-base :nth-child(3)",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--sf-leading, var(--text-base--line-height));
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "nth-[3]:text-base",
                EscapedClassName = @".nth-\[3\]\:text-base:nth-child(3), .nth-\[3\]\:text-base :nth-child(3)",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--sf-leading, var(--text-base--line-height));
                    """,
                IsValid = true
            },
            new ()
            {
                ClassName = "group-hover:underline",
                EscapedClassName = @".group:hover .group-hover\:underline",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
            },
            new ()
            {
                ClassName = "group-hover/my-group:underline",
                EscapedClassName = @".group\/my-group:hover .group-hover\/my-group\:underline",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
            },
            new ()
            {
                ClassName = "group-has-[a]:underline",
                EscapedClassName = @".group-has-\[a\]\:underline:is(:where(.group):has(:is(a)) *)",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
            },
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
                    $"@media {AppRunner.Library.MediaQueryPrefixes["dark"].Statement} {{",
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} and {AppRunner.Library.MediaQueryPrefixes["max-desk"].Statement} {{",
                    "@supports(display:flex) {",
                ]
            },

            new ()
            {
                ClassName = "peer-hover:underline",
                EscapedClassName = @".peer:hover ~ .peer-hover\:underline",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
            },
            new ()
            {
                ClassName = "peer-hover/draft:underline",
                EscapedClassName = @".peer\/draft:hover ~ .peer-hover\/draft\:underline",
                Styles =
                    """
                    text-decoration-line: underline;
                    """,
                IsValid = true,
            },



            new ()
            {
                ClassName = "leading-none",
                EscapedClassName = ".leading-none",
                IsValid = true,
                Styles =
                    """
                    --sf-leading: 1;
                    line-height: var(--sf-leading);
                    """
            },
            new ()
            {
                ClassName = "leading-2",
                EscapedClassName = ".leading-2",
                IsValid = true,
                Styles =
                    """
                    --sf-leading: calc(var(--spacing) * 2);
                    line-height: var(--sf-leading);
                    """
            },
            new ()
            {
                ClassName = "-leading-2",
                EscapedClassName = ".-leading-2",
                IsValid = true,
                Styles =
                    """
                    --sf-leading: calc(var(--spacing) * 2 * -1);
                    line-height: var(--sf-leading);
                    """
            },
            new ()
            {
                ClassName = "leading-[1.35]",
                EscapedClassName = @".leading-\[1\.35\]",
                IsValid = true,
                Styles =
                    """
                    --sf-leading: 1.35;
                    line-height: var(--sf-leading);
                    """
            },
            new ()
            {
                ClassName = "leading-[1.5rem]",
                EscapedClassName = @".leading-\[1\.5rem\]",
                IsValid = true,
                Styles =
                    """
                    --sf-leading: 1.5rem;
                    line-height: var(--sf-leading);
                    """
            },
            new ()
            {
                ClassName = "tabp:text-indigo-400",
                EscapedClassName = @".tabp\:text-indigo-400",
                Styles =
                    "color: var(--color-indigo-400);",
                IsValid = true,
                Wrappers =
                [
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "text-indigo-400/37",
                EscapedClassName = @".text-indigo-400\/37",
                IsValid = true,
                Styles = "color: color-mix(in oklab, var(--color-indigo-400) 37%, transparent);"
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
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
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
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
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
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
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
                    $"@media {AppRunner.Library.MediaQueryPrefixes["tabp"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "text-base",
                EscapedClassName = ".text-base",
                Styles =
                    """
                    font-size: var(--text-base);
                    line-height: var(--sf-leading, var(--text-base--line-height));
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
                    """
                    background-color: var(--color-indigo-400);
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
                Styles = "container: primary / inline-size;",
                IsValid = true
            },
            new ()
            {
                ClassName = "@sm:@max-lg:leading-none",
                EscapedClassName = @".\@sm\:\@max-lg\:leading-none",
                Styles =
                    """
                    --sf-leading: 1;
                    line-height: var(--sf-leading);
                    """,
                IsValid = true,
                Wrappers =
                [
                    $"@container {AppRunner.Library.ContainerQueryPrefixes["@sm"].Statement} and {AppRunner.Library.ContainerQueryPrefixes["@max-lg"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "@sm/primary:@max-lg/primary:leading-none",
                EscapedClassName = @".\@sm\/primary\:\@max-lg\/primary\:leading-none",
                Styles =
                    """
                    --sf-leading: 1;
                    line-height: var(--sf-leading);
                    """,
                IsValid = true,
                Wrappers =
                [
                    $"@container primary {AppRunner.Library.ContainerQueryPrefixes["@sm"].Statement} and {AppRunner.Library.ContainerQueryPrefixes["@max-lg"].Statement} {{"
                ]
            },
            new ()
            {
                ClassName = "leading-none!",
                EscapedClassName = @".leading-none\!",
                Styles =
                    """
                    --sf-leading: 1;
                    line-height: var(--sf-leading) !important;
                    """,
                IsValid = true,
                IsImportant = true
            },
        };

        foreach (var test in testClasses)
        {
            var cssClass = new CssClass(AppRunner, selector: test.ClassName);

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

            TestOutputHelper?.WriteLine($"UtilityClassProcessing() => {test.ClassName}");
        }
    }

    [Fact]
    public void WildcardHandling()
    {
        var cssClass = new CssClass(AppRunner, selector: "*:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@":is(.\*\:whitespace-pre\! > *)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "**:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@":is(.\*\*\:whitespace-pre\! *)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);
    }

    [Fact]
    public void DataAttributeHandling()
    {
        var cssClass = new CssClass(AppRunner, selector: "data-active:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@".data-active\:whitespace-pre\![data-active]", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "dark:lg:data-active:hover:bg-indigo-600");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".dark\:lg\:data-active\:hover\:bg-indigo-600[data-active]:hover", cssClass.EscapedSelector);
        Assert.Equal("background-color: var(--color-indigo-600);", cssClass.Styles);
    }

    [Fact]
    public void NotVariantHandling()
    {
        var cssClass = new CssClass(AppRunner, selector: "not-hover:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@".not-hover\:whitespace-pre\!:not(:hover)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "not-data-active:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@".not-data-active\:whitespace-pre\!:not([data-active])", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);
    }

    [Fact]
    public void ArbitraryNotSelectorVariants()
    {
        // Pseudo-class selector
        var cssClass = new CssClass(AppRunner, selector: "not-[:checked]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\:checked\]\:whitespace-pre:not(:is(:checked))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Class selector
        cssClass = new CssClass(AppRunner, selector: "not-[.group]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\.group\]\:whitespace-pre:not(:is(.group))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Id selector
        cssClass = new CssClass(AppRunner, selector: "not-[#my-id]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\#my-id\]\:whitespace-pre:not(:is(#my-id))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Element selector
        cssClass = new CssClass(AppRunner, selector: "not-[p]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[p\]\:whitespace-pre:not(:is(p))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Universal selector (no &): wrapped in :is()
        cssClass = new CssClass(AppRunner, selector: "not-[*]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\*\]\:whitespace-pre:not(:is(*))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Attribute selector
        cssClass = new CssClass(AppRunner, selector: "not-[[data-active]]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\[data-active\]\]\:whitespace-pre:not(:is([data-active]))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Compound class selector
        cssClass = new CssClass(AppRunner, selector: "not-[.a.b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\.a\.b\]\:whitespace-pre:not(:is(.a.b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Multi pseudo-class (comma list)
        cssClass = new CssClass(AppRunner, selector: "not-[:hover,:focus]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\:hover\,\:focus\]\:whitespace-pre:not(:is(:hover, :focus))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Descendant selector
        cssClass = new CssClass(AppRunner, selector: "not-[a:hover]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\:hover\]\:whitespace-pre:not(:is(a:hover))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Nested :is() is wrapped again (Tailwind behavior)
        cssClass = new CssClass(AppRunner, selector: "not-[:is(.group)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\:is\(\.group\)\]\:whitespace-pre:not(:is(:is(.group)))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Adjacent sibling combinator (underscore = space)
        cssClass = new CssClass(AppRunner, selector: "not-[a_+_b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a_\+_b\]\:whitespace-pre:not(:is(a + b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // General sibling combinator (underscore = space)
        cssClass = new CssClass(AppRunner, selector: "not-[a_~_b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a_\~_b\]\:whitespace-pre:not(:is(a ~ b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Descendant combinator (underscore = space)
        cssClass = new CssClass(AppRunner, selector: "not-[a_>_b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a_\>_b\]\:whitespace-pre:not(:is(a > b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Combinator without underscores: spacing is normalized
        cssClass = new CssClass(AppRunner, selector: "not-[a+b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\+b\]\:whitespace-pre:not(:is(a + b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Sibling combinator without underscores: spacing is normalized
        cssClass = new CssClass(AppRunner, selector: "not-[a~b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\~b\]\:whitespace-pre:not(:is(a ~ b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Trailing comma in the list: empty part is kept
        cssClass = new CssClass(AppRunner, selector: "not-[a,]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\,\]\:whitespace-pre:not(:is(a, ))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Leading comma in the list: empty part is kept
        cssClass = new CssClass(AppRunner, selector: "not-[,a]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\,a\]\:whitespace-pre:not(:is(, a))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Double comma in the list: empty part is kept
        cssClass = new CssClass(AppRunner, selector: "not-[a,,b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\,\,b\]\:whitespace-pre:not(:is(a, , b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Whitespace runs collapse to a single space (descendant combinator)
        cssClass = new CssClass(AppRunner, selector: "not-[a__b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a__b\]\:whitespace-pre:not(:is(a b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Leading whitespace is dropped
        cssClass = new CssClass(AppRunner, selector: "not-[_.group]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[_\.group\]\:whitespace-pre:not(:is(.group))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Trailing whitespace is kept (lightningcss serialization)
        cssClass = new CssClass(AppRunner, selector: "not-[.group_]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\.group_\]\:whitespace-pre:not(:is(.group ))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Invalid selectors are still emitted by Tailwind: a leading combinator
        // keeps its space when preceded by whitespace
        cssClass = new CssClass(AppRunner, selector: "not-[_>img]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[_\>img\]\:whitespace-pre:not(:is( > img))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // A trailing combinator keeps its space (lightningcss serialization)
        cssClass = new CssClass(AppRunner, selector: "not-[.foo_>]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\.foo_\>\]\:whitespace-pre:not(:is(.foo > ))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // A list part starting with a combinator keeps its leading space
        cssClass = new CssClass(AppRunner, selector: "not-[a,>b]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[a\,\>b\]\:whitespace-pre:not(:is(a,  > b))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
    }

    [Fact]
    public void ArbitraryNotRelativeSelectorVariants()
    {
        // Bare & refers to the element itself: becomes *
        var cssClass = new CssClass(AppRunner, selector: "not-[&]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\]\:whitespace-pre:not(*)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & followed by a pseudo-class: the & is stripped
        cssClass = new CssClass(AppRunner, selector: "not-[&:hover]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\:hover\]\:whitespace-pre:not(:hover)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & followed by multiple pseudo-classes
        cssClass = new CssClass(AppRunner, selector: "not-[&:hover:focus]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\:hover\:focus\]\:whitespace-pre:not(:hover:focus)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & followed by a class: the & is stripped
        cssClass = new CssClass(AppRunner, selector: "not-[&.active]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\.active\]\:whitespace-pre:not(.active)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & followed by an id selector
        cssClass = new CssClass(AppRunner, selector: "not-[&#id]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\#id\]\:whitespace-pre:not(#id)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & followed by an attribute selector
        cssClass = new CssClass(AppRunner, selector: "not-[&[data-x]]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\[data-x\]\]\:whitespace-pre:not([data-x])", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & with a combinator keeps the universal selector
        cssClass = new CssClass(AppRunner, selector: "not-[&_>.foo]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&_\>\.foo\]\:whitespace-pre:not(* > .foo)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // & with a combinator and no underscores: spacing is normalized
        cssClass = new CssClass(AppRunner, selector: "not-[&>.foo]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\>\.foo\]\:whitespace-pre:not(* > .foo)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Comma-separated list with &
        cssClass = new CssClass(AppRunner, selector: "not-[&:hover,:focus]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\:hover\,\:focus\]\:whitespace-pre:not(:hover, :focus)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Trailing comma in the list with &: empty part is kept
        cssClass = new CssClass(AppRunner, selector: "not-[&:hover,]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\&\:hover\,\]\:whitespace-pre:not(:hover, )", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Leading comma in the list with &: empty part is kept
        cssClass = new CssClass(AppRunner, selector: "not-[,&.a]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\,\&\.a\]\:whitespace-pre:not(, .a)", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
    }

    [Fact]
    public void ArbitraryNotAtRuleVariants()
    {
        // @media negation
        var cssClass = new CssClass(AppRunner, selector: "not-[@media_print]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".not-\[\@media_print\]\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media not print {", cssClass.Wrappers.First().Value);

        // @media with a parenthesized condition (no space after the at-rule name)
        cssClass = new CssClass(AppRunner, selector: "not-[@media(orientation:portrait)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media not (orientation:portrait) {", cssClass.Wrappers.First().Value);

        // @media with a parenthesized condition (underscore-separated)
        cssClass = new CssClass(AppRunner, selector: "not-[@media_(orientation:landscape)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media not (orientation:landscape) {", cssClass.Wrappers.First().Value);

        // Double negation cancels out
        cssClass = new CssClass(AppRunner, selector: "not-[@media_not_(orientation:portrait)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (orientation:portrait) {", cssClass.Wrappers.First().Value);

        // @supports negation
        cssClass = new CssClass(AppRunner, selector: "not-[@supports(display:grid)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@supports not (display:grid) {", cssClass.Wrappers.First().Value);

        // @container negation (unnamed)
        cssClass = new CssClass(AppRunner, selector: "not-[@container_style(--a)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container not style(--a) {", cssClass.Wrappers.First().Value);

        // @container negation (named container)
        cssClass = new CssClass(AppRunner, selector: "not-[@container_card_style(--c)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container card not style(--c) {", cssClass.Wrappers.First().Value);

        // @container double negation cancels out
        cssClass = new CssClass(AppRunner, selector: "not-[@container_not_style(--b)]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container style(--b) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void ArbitraryNotVariantStackingAndImportant()
    {
        // Stacked with another pseudo-class variant
        var cssClass = new CssClass(AppRunner, selector: "hover:not-[.group]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".hover\:not-\[\.group\]\:whitespace-pre:hover:not(:is(.group))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);

        // Stacked with a media variant (dark)
        cssClass = new CssClass(AppRunner, selector: "dark:not-[:checked]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".dark\:not-\[\:checked\]\:whitespace-pre:not(:is(:checked))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Single(cssClass.Wrappers);

        // Important modifier
        cssClass = new CssClass(AppRunner, selector: "not-[.group]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.True(cssClass.IsImportant);
        Assert.Equal(@".not-\[\.group\]\:whitespace-pre\!:not(:is(.group))", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre !important;", cssClass.Styles);
    }

    [Fact]
    public void ArbitraryNotInvalidVariants()
    {
        // Empty value
        var cssClass = new CssClass(AppRunner, selector: "not-[]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);

        // Pseudo-elements cannot be negated
        cssClass = new CssClass(AppRunner, selector: "not-[::before]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);

        // Relative combinators cannot be negated
        cssClass = new CssClass(AppRunner, selector: "not-[>img]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);

        cssClass = new CssClass(AppRunner, selector: "not-[+img]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);

        cssClass = new CssClass(AppRunner, selector: "not-[~img]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);

        // not-* variants do not accept modifiers
        cssClass = new CssClass(AppRunner, selector: "not-[:checked]/foo:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);
    }

    [Fact]
    public void Containers()
    {
        var cssClass = new CssClass(AppRunner, selector: "@max-md:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container (width < 28rem) {", cssClass.Wrappers.First().Value);

        cssClass = new CssClass(AppRunner, selector: "@sm:@max-md:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container (width >= 24rem) and (width < 28rem) {", cssClass.Wrappers.ElementAt(0).Value);
        
        cssClass = new CssClass(AppRunner, selector: "@sm:@max-md:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@container (width >= 24rem) and (width < 28rem) {", cssClass.Wrappers.ElementAt(0).Value);
    }

    [Fact]
    public void ArbitraryCss()
    {
        var cssClass = new CssClass(AppRunner, selector: "[@media_(width_>=_600px)]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (width >= 600px) {", cssClass.Wrappers.First().Value);

        cssClass = new CssClass(AppRunner, selector: "min-[600px]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (width >= 600px) {", cssClass.Wrappers.First().Value);

        cssClass = new CssClass(AppRunner, selector: "max-[600px]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (width < 600px) {", cssClass.Wrappers.First().Value);

        cssClass = new CssClass(AppRunner, selector: "min-[320px]:max-[600px]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(2, cssClass.Wrappers.Count);
        Assert.Equal("@media (width >= 320px) {", cssClass.Wrappers.ElementAt(0).Value);
        Assert.Equal("@media (width < 600px) {", cssClass.Wrappers.ElementAt(1).Value);

        cssClass = new CssClass(AppRunner, selector: "w-[calc(100vw-(--nav-width))]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal("width: calc(100vw-var(--nav-width));", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "w-[calc(100vw-var(--nav-width))]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal("width: calc(100vw-var(--nav-width));", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "w-[calc(100vw_-_var(--nav-width))]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal("width: calc(100vw - var(--nav-width));", cssClass.Styles);

        cssClass = new CssClass(AppRunner, selector: "[--my-value:1rem]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal("--my-value: 1rem;", cssClass.Styles);
        
        cssClass = new CssClass(AppRunner, selector: "bg-[url(/\\_content/images/bg.png)]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal("background-image: url(/_content/images/bg.png);", cssClass.Styles);
    }

    [Fact]
    public void ArbitraryRazorCss()
    {
        var cssClass = new CssClass(AppRunner, selector: "[@@media_(width_>=_600px)]:whitespace-pre!");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (width >= 600px) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void Env()
    {
        var cssClass = new CssClass(AppRunner, selector: "tabp:pt-[env(safe-area-inset-top)]");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Single(cssClass.Wrappers);
        Assert.Equal("@media (min-aspect-ratio: 0.625) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void Supports()
    {
        var cssClass = new CssClass(AppRunner, selector: "supports-[color:red]:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".supports-\[color\:red\]\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@supports (color:red) {", cssClass.Wrappers.First().Value);

        cssClass = new CssClass(AppRunner, selector: "supports-backdrop-blur:whitespace-pre");
        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".supports-backdrop-blur\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@supports ((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0)) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void Pointer()
    {
        var cssClass = new CssClass(AppRunner, selector: "pointer-fine:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".pointer-fine\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@media (pointer: fine) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void NoScript()
    {
        var cssClass = new CssClass(AppRunner, selector: "noscript:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".noscript\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@media (scripting: none) {", cssClass.Wrappers.First().Value);
    }

    [Fact]
    public void StartingStyle()
    {
        var cssClass = new CssClass(AppRunner, selector: "starting:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".starting\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@starting-style {", cssClass.Wrappers.First().Value);
    }
    
    [Fact]
    public void LeadingNumericEscape()
    {
        var cssClass = new CssClass(AppRunner, selector: "2xl:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".\32 xl\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
    }
    
    [Fact]
    public void BreakpointRange()
    {
        var cssClass = new CssClass(AppRunner, selector: "lg:max-xl:whitespace-pre");

        Assert.NotNull(cssClass);
        Assert.True(cssClass.IsValid);
        Assert.Equal(@".lg\:max-xl\:whitespace-pre", cssClass.EscapedSelector);
        Assert.Equal("white-space: pre;", cssClass.Styles);
        Assert.Equal("@media (width >= 64rem) and (width < 80rem) {", cssClass.Wrappers.First().Value);
    }
}
