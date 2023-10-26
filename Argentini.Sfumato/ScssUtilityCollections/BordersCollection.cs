using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class BordersCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllBordersClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 200000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddRoundedGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBorderGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddDivideGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddOutlineGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddRingGroupAsync(sortSeed);

        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddRoundedGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var variations = new Dictionary<string, string>
        {
            ["rounded"] = "border-radius: {value};",
            ["rounded-s"] = """
                            border-start-start-radius: {value};
                            border-end-start-radius: {value};
                            """,
            ["rounded-e"] = """
                            border-start-end-radius: {value};
                            border-end-end-radius: {value};
                            """,
            ["rounded-t"] = """
                            border-top-left-radius: {value};
                            border-top-right-radius: {value};
                            """,
            ["rounded-r"] = """
                            border-top-right-radius: {value};
                            border-bottom-right-radius: {value};
                            """,
            ["rounded-b"] = """
                            border-bottom-right-radius: {value};
                            border-bottom-left-radius: {value};
                            """,
            ["rounded-l"] = """
                            border-top-left-radius: {value};
                            border-bottom-left-radius: {value};
                            """,
            ["rounded-ss"] = "border-start-start-radius: {value};",
            ["rounded-se"] = "border-start-end-radius: {value};",
            ["rounded-ee"] = "border-end-end-radius: {value};",
            ["rounded-es"] = "border-end-start-radius: {value};",
            ["rounded-tl"] = "border-top-left-radius: {value};",
            ["rounded-tr"] = "border-top-right-radius: {value};",
            ["rounded-br"] = "border-bottom-right-radius: {value};",
            ["rounded-bl"] = "border-bottom-left-radius: {value};"
        };

        foreach (var (selector, scssTemplate) in variations)
        {
            var scssUtilityClassGroup = new ScssUtilityClassGroup
            {
                SelectorPrefix = selector
            };
        
            #region Arbitrary Value Options

            sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", scssTemplate, sortSeed);

            #endregion
        
            sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                SfumatoScss.RoundedOptions,
                scssTemplate,
                false,
                sortSeed
            );

            if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        }
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBorderGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "border-width: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "border-color: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.BorderWidthOptions,
            "border-width: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["solid"] = "solid",
                ["dashed"] = "dashed",
                ["dotted"] = "dotted",
                ["double"] = "double",
                ["hidden"] = "hidden",
                ["none"] = "none"
            },
            "border-style: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "border-color: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        var variations = new Dictionary<string, string>
        {
            ["border-x"] = """
                            border-left-width: {value};
                            border-right-width: {value};
                            """,
            ["border-y"] = """
                            border-top-width: {value};
                            border-bottom-width: {value};
                            """,
            ["border-s"] = "border-inline-start-width: {value};",
            ["border-e"] = "border-inline-end-width: {value};",
            ["border-t"] = "border-top-width: {value};",
            ["border-r"] = "border-right-width: {value};",
            ["border-b"] = "border-bottom-width: {value};",
            ["border-l"] = "border-left-width: {value};"
        };

        foreach (var (selector, scssTemplate) in variations)
        {
            scssUtilityClassGroup = new ScssUtilityClassGroup
            {
                SelectorPrefix = selector
            };
        
            #region Arbitrary Value Options

            sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", scssTemplate, sortSeed);
            sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", scssTemplate.Replace("width:", "color:"), sortSeed);

            #endregion
        
            sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                SfumatoScss.BorderWidthOptions,
                scssTemplate,
                false,
                sortSeed
            );
            
            sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                SfumatoScss.Colors,
                scssTemplate.Replace("width:", "color:"),
                false,
                sortSeed
            );

            if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        }
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddDivideGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Divide
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "color",
            """
            & > * + * {
                border-color: {value};
            }
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            """
            & > * + * {
                border-color: {value};
            }
            """,
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["solid"] = "solid",
                ["dashed"] = "dashed",
                ["dotted"] = "dotted",
                ["double"] = "double",
                ["none"] = "none"
            },
            """
            & > * + * {
                border-style: {value};
            }
            """,
            false,
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Divide X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide-x"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.DivideWidthOptions,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Divide Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide-y"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                border-top-width: {value};
                border-bottom-width: 0px;
            }
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.DivideWidthOptions,
            """
            & > * + * {
                border-top-width: {value};
                border-bottom-width: 0px;
            }
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["divide-y-reverse"] = "--sf-divide-y-reverse: 1",
                ["divide-x-reverse"] = "--sf-divide-x-reverse: 1"
            },
            """
            & > * + * {
                {value};
            }
            """,
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddOutlineGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "outline"
        };
        
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "outline-width: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "outline-color: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "outline-style: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "border-style: {value};",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "outline-color: {value};",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["dashed"] = "dashed",
                ["dotted"] = "dotted",
                ["double"] = "double",
                ["none"] = "none"
            },
            "outline-style: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "outline-offset"
        };
        
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "outline-offset: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "outline-offset: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddRingGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Ring
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring",
            Category = "ring"
        };
        
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "--sf-ring-color: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "0.1875rem",
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "--sf-ring-color: {value};",
            false,
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Ring Inset
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring-inset",
            Category = "ring"
        };
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "inset"
            },
            "--sf-ring-inset: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion        
        
        #region Ring Offset
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring-offset"
        };
        
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            --sf-ring-offset-width: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """,
            sortSeed
            );
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "color",
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """,
            sortSeed
            );

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            """
            --sf-ring-offset-width: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """,
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """,
            false,
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}