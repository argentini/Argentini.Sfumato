using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class SpacingCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllSpacingClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 900000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddPaddingGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddMarginGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddSpaceGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddPaddingGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Padding
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "p"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "padding: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "px"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            padding-left: {value};
            padding-right: {value};
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            padding-left: {value};
            padding-right: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            padding-left: {value};
            padding-right: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "py"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            padding-top: {value};
            padding-bottom: {value};
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            padding-top: {value};
            padding-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            padding-top: {value};
            padding-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Inline Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ps"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-inline-start: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-inline-start: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-inline-start: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Inline End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pe"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-inline-end: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-inline-end: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-inline-end: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Top
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pt"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-top: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-top: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-top: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pr"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-right: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-right: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-right: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Padding Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pb"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-bottom: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-bottom: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-bottom: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Padding Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pl"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-left: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-left: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-left: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddMarginGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Margin
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "m"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "margin: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mx"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            margin-left: {value};
            margin-right: {value};
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            margin-left: {value};
            margin-right: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            margin-left: {value};
            margin-right: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "my"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            margin-top: {value};
            margin-bottom: {value};
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            margin-top: {value};
            margin-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            margin-top: {value};
            margin-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Inline Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ms"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-inline-start: {value};", sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-inline-start: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-inline-start: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Inline End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "me"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-inline-end: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-inline-end: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-inline-end: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Top
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mt"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-top: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-top: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-top: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mr"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-right: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-right: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-right: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Margin Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mb"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-bottom: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-bottom: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-bottom: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Margin Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ml"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-left: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-left: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-left: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddSpaceGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Space X
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "space-x"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                margin-left: {value};
            }
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            & > * + * {
                margin-left: {value};
            }
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            & > * + * {
                margin-left: {value};
            }
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Space Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "space-y"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                margin-top: {value};
            }
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            & > * + * {
                margin-top: {value};
            }
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            & > * + * {
                margin-top: {value};
            }
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}