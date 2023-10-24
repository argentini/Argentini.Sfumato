using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class BordersCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllBordersClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddRoundedGroupAsync());
        tasks.Add(collection.AddBorderGroupAsync());
        tasks.Add(collection.AddBorderColorGroupAsync());
        tasks.Add(collection.AddDivideGroupAsync());
        tasks.Add(collection.AddOutlineGroupAsync());
        tasks.Add(collection.AddRingGroupAsync());
    }
    
    public static async Task AddRoundedGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
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
            var scssUtilityClass = new ScssUtilityClassGroup
            {
                SelectorPrefix = selector
            };
        
            #region Arbitrary Value Options

            await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", scssTemplate);

            #endregion
        
            await scssUtilityClass.AddClassesAsync(
                SfumatoScss.RoundedOptions,
                scssTemplate
            );

            collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        }
    }
    
    public static async Task AddBorderGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "border-width: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.RoundedOptions,
            "border-width: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["solid"] = "solid",
                ["dashed"] = "dashed",
                ["dotted"] = "dotted",
                ["double"] = "double",
                ["hidden"] = "hidden",
                ["none"] = "none"
            },
            "border-style: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
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
            scssUtilityClass = new ScssUtilityClassGroup
            {
                SelectorPrefix = selector
            };
        
            #region Arbitrary Value Options

            await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", scssTemplate);

            #endregion
        
            await scssUtilityClass.AddClassesAsync(
                SfumatoScss.RoundedOptions,
                scssTemplate
            );

            collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        }
    }
    
    public static async Task AddBorderColorGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var variations = new Dictionary<string, string>
        {
            ["border"] = "border-color: {value};",
            ["border-x"] = """
                           border-left-color: {value};
                           border-right-color: {value};
                           """,
            ["border-y"] = """
                           border-top-color: {value};
                           border-bottom-color: {value};
                           """,
            ["border-s"] = "border-inline-start-color: {value};",
            ["border-e"] = "border-inline-end-color: {value};",
            ["border-t"] = "border-top-color: {value};",
            ["border-r"] = "border-right-color: {value};",
            ["border-b"] = "border-bottom-color: {value};",
            ["border-l"] = "border-left-color: {value};"
        };

        foreach (var (selector, scssTemplate) in variations)
        {
            var scssUtilityClass = new ScssUtilityClassGroup
            {
                SelectorPrefix = selector
            };
        
            #region Arbitrary Value Options

            await scssUtilityClass.AddAbitraryValueClassAsync("color", scssTemplate);

            #endregion
        
            await scssUtilityClass.AddClassesAsync(
                SfumatoScss.Colors,
                scssTemplate
            );

            collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        }
    }
    
    public static async Task AddDivideGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Divide
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "color",
            """
            & > * + * {
                border-color: {value};
            }
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            """
            & > * + * {
                border-color: {value};
            }
            """
        );

        await scssUtilityClass.AddClassesAsync(
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
            """
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion

        #region Divide X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide-x"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.DivideWidthOptions,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
        
        #region Divide Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "divide-y"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                border-top-width: {value};
                border-bottom-width: 0px;
            }
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.DivideWidthOptions,
            """
            & > * + * {
                border-top-width: {value};
                border-bottom-width: 0px;
            }
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
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
            """
        );
        
    }
    
    public static async Task AddOutlineGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "outline"
        };
        
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "outline-width: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("color", "outline-color: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "outline-style: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "border-style: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "outline-color: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["dashed"] = "dashed",
                ["dotted"] = "dotted",
                ["double"] = "double",
                ["none"] = "none"
            },
            "outline-style: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "outline-offset"
        };
        
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "outline-offset: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "outline-offset: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddRingGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Ring
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring",
            Category = "ring"
        };
        
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);");
        await scssUtilityClass.AddAbitraryValueClassAsync("color", "--sf-ring-color: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "0.1875rem",
                ["0"] = "0px",
                ["1"] = "1px",
                ["2"] = 2.PxToRem(),
                ["4"] = 4.PxToRem(),
                ["8"] = 8.PxToRem()
            },
            "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);"
        );

        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "--sf-ring-color: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
        
        #region Ring Inset
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring-inset",
            Category = "ring"
        };
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "inset"
            },
            "--sf-ring-inset: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion        
        
        #region Ring Offset
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ring-offset"
        };
        
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            --sf-ring-offset-width: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """
            );
        await scssUtilityClass.AddAbitraryValueClassAsync(
            "color",
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """
            );

        #endregion

        await scssUtilityClass.AddClassesAsync(
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
            """
        );

        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
    }
}