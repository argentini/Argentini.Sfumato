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

        await internalCollection.AddFontGroupAsync();
        await internalCollection.AddAntialiasedGroupAsync();
        await internalCollection.AddTrackingGroupAsync();
        await internalCollection.AddLineClampGroupAsync();
        await internalCollection.AddLeadingGroupAsync();
        await internalCollection.AddListGroupAsync();
        await internalCollection.AddTextGroupAsync();
        await internalCollection.AddTextDecorationLineGroupAsync();
        await internalCollection.AddTextDecorationGroupAsync();
        await internalCollection.AddTextUnderlineOffsetGroupAsync();
        await internalCollection.AddTextTransformGroupAsync();
        await internalCollection.AddTruncateGroupAsync();
        await internalCollection.AddTextOverflowGroupAsync();
        await internalCollection.AddTextIndentGroupAsync();
        await internalCollection.AddVerticalAlignGroupAsync();
        await internalCollection.AddWhitespaceGroupAsync();
        await internalCollection.AddWordBreakGroupAsync();
        await internalCollection.AddHyphensGroupAsync();
        await internalCollection.AddContentGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }

    public static async Task AddFontGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "font"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "font-weight: {value};");
        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "font-family: {value};");

        #endregion
        
        #region Family
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["sans"] = "ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\"",
                ["serif"] = "ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif",
                ["mono"] = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace"
            },
            "font-family: {value};"
        );
        
        #endregion
        
        #region Style
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["italic"] = "italic",
                ["not-italic"] = "normal"
            },
            "font-style: {value};"
        );
        
        #endregion
        
        #region Weight
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "font-weight: {value};"
        );
        
        #endregion
        
        #region Variant Numeric
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "font-variant-numeric: {value};"
        );
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddAntialiasedGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["antialiased"] = ""
            },
            """
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            """
        );
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["subpixel-antialiased"] = ""
            },
            """
            -webkit-font-smoothing: auto;
            -moz-osx-font-smoothing: auto;
            """
        );
    }

    public static async Task AddTrackingGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "tracking"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "letter-spacing: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["tighter"] = "-0.05em",
                ["tight"] = "-0.025em",
                ["normal"] = "0em",
                ["wide"] = "0.025em",
                ["wider"] = "0.05em",
                ["widest"] = "0.1em"
            },
            "letter-spacing: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddLineClampGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "line-clamp"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "integer",
            """
                       -webkit-line-clamp: {value};
                       overflow: hidden;
                       display: -webkit-box;
                       -webkit-box-orient: vertical;
                       """
            );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = ""
            },
            """
               overflow: visible;
               display: block;
               -webkit-box-orient: horizontal;
               -webkit-line-clamp: none;
               """
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddLeadingGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "leading"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,integer", "line-height: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "1",
                ["tight"] = "1.25",
                ["snug"] = "1.375",
                ["normal"] = "1.5",
                ["relaxed"] = "1.625",
                ["loose"] = "2"
            },
            "line-height: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(3m, 10m, 1m),
            "line-height: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddListGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "list"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("url", "list-style-image: {value};");
        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "list-style-type: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none"
            },
            "list-style-image: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["inside"] = "inside",
                ["outside"] = "outside"
            },
            "list-style-position: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
                ["disc"] = "disc",
                ["decimal"] = "decimal"
            },
            "list-style-type: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddTextGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "text"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "color: {value};");
        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "font-size: {value};");

        foreach (var size in SfumatoScss.TextSizes)
        {
            await scssUtilityClassGroup.AddAbitrarySlashValueClassAsync(
                size.Key,
                "length,percentage,number",
                $$"""
                  font-size: {{size.Value}};
                  line-height: {value};
                  """
            );
        }
        
        #endregion
        
        #region Size And Leading
        
        foreach (var size in SfumatoScss.TextSizes)
        {
            await scssUtilityClassGroup.AddClassesAsync(
                new Dictionary<string,string>
                {
                    [size.Key] = size.Value
                },
                "font-size: {value};"
            );

            foreach (var leading in SfumatoScss.Leading)
            {
                await scssUtilityClassGroup.AddClassesAsync(
                    new Dictionary<string,string>
                    {
                        [$"{size.Key}/{leading.Key}"] = size.Value
                    },
                    $$"""
                      font-size: {value};
                      line-height: {{leading.Value}};
                      """
                );
            }
        }
        
        #endregion
        
        #region Family
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["sans"] = "ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\"",
                ["serif"] = "ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif",
                ["mono"] = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace"
            },
            "font-family: {value};"
        );
        
        #endregion
        
        #region Style
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["italic"] = "italic",
                ["not-italic"] = "normal"
            },
            "font-style: {value};"
        );
        
        #endregion
        
        #region Weight
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "font-weight: {value};"
        );
        
        #endregion
        
        #region Variant Numeric
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "font-variant-numeric: {value};"
        );
        
        #endregion

        #region Align
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["left"] = "left",
                ["center"] = "center",
                ["right"] = "right",
                ["justify"] = "justify",
                ["start"] = "start",
                ["end"] = "end"
            },
            "text-align: {value};"
        );
        
        #endregion
        
        #region Color

        await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "color: {value};"
        );
            
        #endregion

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddTextDecorationLineGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["underline"] = "underline",
                ["overline"] = "overline",
                ["line-through"] = "line-through",
                ["no-underline"] = "none"
            },
            "text-decoration-line: {value};"
        );
    }
    
    public static async Task AddTextDecorationGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "decoration"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "text-decoration-color: {value};");
        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-decoration-thickness: {value};");

        #endregion
        
        #region Color

        await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "text-decoration-color: {value};"
        );
            
        #endregion

        #region Style
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["solid"] = "solid",
                ["double"] = "double",
                ["dotted"] = "dotted",
                ["dashed"] = "dashed",
                ["wavy"] = "wavy"
            },
            "text-decoration-style: {value};"
        );
        
        #endregion

        #region Thickness
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "text-decoration-thickness: {value};"
        );
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddTextUnderlineOffsetGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "underline-offset"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-underline-offset: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "text-decoration-style: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddTextTransformGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["uppercase"] = "uppercase",
                ["lowercase"] = "lowercase",
                ["capitalize"] = "capitalize",
                ["normal-case"] = "none"
            },
            "text-transform: {value};"
        );
    }
    
    public static async Task AddTruncateGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["truncate"] = ""
            },
            """
               overflow: hidden;
               text-overflow: ellipsis;
               white-space: nowrap;
               """
        );
    }
    
    public static async Task AddTextOverflowGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["text-ellipsis"] = "ellipsis",
                ["text-clip"] = "clip",
            },
            "text-overflow: {value};"
        );
    }

    public static async Task AddTextIndentGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "indent"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "text-indent: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "text-indent: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "text-indent: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddVerticalAlignGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "align"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "vertical-align: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "vertical-align: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddWhitespaceGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "whitespace"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["normal"] = "normal",
                ["nowrap"] = "nowrap",
                ["pre"] = "pre",
                ["pre-line"] = "pre-line",
                ["pre-wrap"] = "pre-wrap",
                ["break-spaces"] = "break-spaces"
            },
            "white-space: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddWordBreakGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["break-normal"] = ""
            },
            """
            overflow-wrap: normal;
            word-break: normal;
            """
        );
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["break-words"] = "break-word"
            },
            "overflow-wrap: {value};"
        );
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["all"] = "break-all",
                ["keep"] = "keep-all"
            },
            "word-break: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddHyphensGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "hyphens"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
                ["manual"] = "manual",
                ["auto"] = "auto"
            },
            "hyphens: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddContentGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "content"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("string", "content: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none",
            },
            "content: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
}