using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TypographyCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllTypographyClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 1400000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddFontGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddAntialiasedGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTrackingGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddLineClampGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddLeadingGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddListGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextDecorationLineGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextDecorationGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextUnderlineOffsetGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextTransformGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTruncateGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextOverflowGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTextIndentGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddVerticalAlignGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddWhitespaceGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddWordBreakGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddHyphensGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddContentGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }

    public static async Task<int> AddFontGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "font"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "font-weight: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "font-family: {value};", sortSeed);

        #endregion
        
        #region Family
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["sans"] = "ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\"",
                ["serif"] = "ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif",
                ["mono"] = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace"
            },
            "font-family: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Style
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["italic"] = "italic",
                ["not-italic"] = "normal"
            },
            "font-style: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Weight
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["thin"] = "100",
                ["extralight"] = "200",
                ["light"] = "300",
                ["normal"] = "400",
                ["medium"] = "500",
                ["semibold"] = "600",
                ["bold"] = "700",
                ["extrabold"] = "800",
                ["black"] = "900"
            },
            "font-weight: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Variant Numeric
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["normal-nums"] = "normal",
                ["ordinal"] = "ordinal",
                ["slashed-zero"] = "slashed-zero",
                ["lining-sums"] = "lining-sums",
                ["oldstyle-nums"] = "oldstyle-nums",
                ["proportional-nums"] = "proportional-nums",
                ["tabular-nums"] = "tabular-nums",
                ["diagonal-fractions"] = "diagonal-fractions",
                ["stacked-fractions"] = "stacked-fractions"
            },
            "font-variant-numeric: {value};",
            sortSeed
        );
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddAntialiasedGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["antialiased"] = ""
            },
            """
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            """,
            sortSeed
        );
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["subpixel-antialiased"] = ""
            },
            """
            -webkit-font-smoothing: auto;
            -moz-osx-font-smoothing: auto;
            """,
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddTrackingGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "tracking"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "letter-spacing: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["tighter"] = "-0.05em",
                ["tight"] = "-0.025em",
                ["normal"] = "0em",
                ["wide"] = "0.025em",
                ["wider"] = "0.05em",
                ["widest"] = "0.1em"
            },
            "letter-spacing: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddLineClampGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "line-clamp"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "integer",
            """
            -webkit-line-clamp: {value};
            overflow: hidden;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = ""
            },
            """
            overflow: visible;
            display: block;
            -webkit-box-orient: horizontal;
            -webkit-line-clamp: none;
            """,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddLeadingGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "leading"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,integer", "line-height: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "1",
                ["tight"] = "1.25",
                ["snug"] = "1.375",
                ["normal"] = "1.5",
                ["relaxed"] = "1.625",
                ["loose"] = "2"
            },
            "line-height: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(3m, 10m, 1m),
            "line-height: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddListGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "list"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("url", "list-style-image: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "list-style-type: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none"
            },
            "list-style-image: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["inside"] = "inside",
                ["outside"] = "outside"
            },
            "list-style-position: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
                ["disc"] = "disc",
                ["decimal"] = "decimal"
            },
            "list-style-type: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTextGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "text"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "color: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "font-size: {value};", sortSeed);

        foreach (var size in SfumatoScss.TextSizes)
        {
            await scssUtilityClassGroup.AddAbitrarySlashValueClassAsync(
                size.Key,
                "length,percentage,number",
                $$"""
                font-size: {{size.Value}};
                line-height: {value};
                """,
                sortSeed
            );
        }
        
        #endregion
        
        #region Size And Leading
        
        foreach (var size in SfumatoScss.TextSizes)
        {
            sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                new Dictionary<string,string>
                {
                    [size.Key] = size.Value
                },
                "font-size: {value};",
                sortSeed
            );

            foreach (var leading in SfumatoScss.Leading)
            {
                sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                    new Dictionary<string,string>
                    {
                        [$"{size.Key}/{leading.Key}"] = size.Value
                    },
                    $$"""
                    font-size: {value};
                    line-height: {{leading.Value}};
                    """,
                    sortSeed
                );
            }
        }
        
        #endregion
        
        #region Family
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["sans"] = "ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\"",
                ["serif"] = "ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif",
                ["mono"] = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace"
            },
            "font-family: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Style
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["italic"] = "italic",
                ["not-italic"] = "normal"
            },
            "font-style: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Weight
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["thin"] = "100",
                ["extralight"] = "200",
                ["light"] = "300",
                ["normal"] = "400",
                ["medium"] = "500",
                ["semibold"] = "600",
                ["bold"] = "700",
                ["extrabold"] = "800",
                ["black"] = "900"
            },
            "font-weight: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Variant Numeric
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["normal-nums"] = "normal",
                ["ordinal"] = "ordinal",
                ["slashed-zero"] = "slashed-zero",
                ["lining-sums"] = "lining-sums",
                ["oldstyle-nums"] = "oldstyle-nums",
                ["proportional-nums"] = "proportional-nums",
                ["tabular-nums"] = "tabular-nums",
                ["diagonal-fractions"] = "diagonal-fractions",
                ["stacked-fractions"] = "stacked-fractions"
            },
            "font-variant-numeric: {value};",
            sortSeed
        );
        
        #endregion

        #region Align
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["left"] = "left",
                ["center"] = "center",
                ["right"] = "right",
                ["justify"] = "justify",
                ["start"] = "start",
                ["end"] = "end"
            },
            "text-align: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Color

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "color: {value};",
            sortSeed
        );
            
        #endregion

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTextDecorationLineGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["underline"] = "underline",
                ["overline"] = "overline",
                ["line-through"] = "line-through",
                ["no-underline"] = "none"
            },
            "text-decoration-line: {value};",
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTextDecorationGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "decoration"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "text-decoration-color: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-decoration-thickness: {value};", sortSeed);

        #endregion
        
        #region Color

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "text-decoration-color: {value};",
            sortSeed
        );
            
        #endregion

        #region Style
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["solid"] = "solid",
                ["double"] = "double",
                ["dotted"] = "dotted",
                ["dashed"] = "dashed",
                ["wavy"] = "wavy"
            },
            "text-decoration-style: {value};",
            sortSeed
        );
        
        #endregion

        #region Thickness
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["auto"] = "auto",
                ["from-font"] = "from-font",
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "text-decoration-thickness: {value};",
            sortSeed
        );
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddTextUnderlineOffsetGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "underline-offset"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-underline-offset: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "text-decoration-style: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTextTransformGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["uppercase"] = "uppercase",
                ["lowercase"] = "lowercase",
                ["capitalize"] = "capitalize",
                ["normal-case"] = "none"
            },
            "text-transform: {value};",
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTruncateGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["truncate"] = ""
            },
            """
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            """,
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTextOverflowGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["text-ellipsis"] = "ellipsis",
                ["text-clip"] = "clip",
            },
            "text-overflow: {value};",
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddTextIndentGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "indent"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-indent: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "text-indent: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "text-indent: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddVerticalAlignGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "align"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "vertical-align: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["baseline"] = "baseline",
                ["top"] = "top",
                ["middle"] = "middle",
                ["bottom"] = "bottom",
                ["text-top"] = "text-top",
                ["text-bottom"] = "text-bottom",
                ["sub"] = "sub",
                ["super"] = "super"
            },
            "vertical-align: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddWhitespaceGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "whitespace"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["normal"] = "normal",
                ["nowrap"] = "nowrap",
                ["pre"] = "pre",
                ["pre-line"] = "pre-line",
                ["pre-wrap"] = "pre-wrap",
                ["break-spaces"] = "break-spaces"
            },
            "white-space: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddWordBreakGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["break-normal"] = ""
            },
            """
            overflow-wrap: normal;
            word-break: normal;
            """,
            sortSeed
        );
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["break-words"] = "break-word"
            },
            "overflow-wrap: {value};",
            sortSeed
        );
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["all"] = "break-all",
                ["keep"] = "keep-all"
            },
            "word-break: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddHyphensGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "hyphens"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
                ["manual"] = "manual",
                ["auto"] = "auto"
            },
            "hyphens: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddContentGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "content"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("string", "content: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
            },
            "content: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
}