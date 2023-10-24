using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class SpacingCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllSpacingClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddPaddingGroupAsync());
        tasks.Add(collection.AddMarginGroupAsync());
        tasks.Add(collection.AddSpaceGroupAsync());
    }
    
    public static async Task AddPaddingGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Padding
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "p"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "padding: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "px"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            padding-left: {value};
            padding-right: {value};
            """
            );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            padding-left: {value};
            padding-right: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            padding-left: {value};
            padding-right: {value};
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "py"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            padding-top: {value};
            padding-bottom: {value};
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            padding-top: {value};
            padding-bottom: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            padding-top: {value};
            padding-bottom: {value};
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Inline Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ps"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-inline-start: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-inline-start: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-inline-start: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Inline End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pe"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-inline-end: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-inline-end: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-inline-end: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Top
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pt"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-top: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-top: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-top: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Right
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pr"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-right: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-right: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-right: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Padding Bottom
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pb"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-bottom: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-bottom: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-bottom: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion

        #region Padding Left
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pl"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "padding-left: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "padding-left: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "padding-left: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }

    public static async Task AddMarginGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Margin
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "m"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "margin: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mx"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            margin-left: {value};
            margin-right: {value};
            """
            );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            margin-left: {value};
            margin-right: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            margin-left: {value};
            margin-right: {value};
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "my"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            margin-top: {value};
            margin-bottom: {value};
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            margin-top: {value};
            margin-bottom: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            margin-top: {value};
            margin-bottom: {value};
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Inline Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ms"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-inline-start: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-inline-start: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-inline-start: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Inline End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "me"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-inline-end: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-inline-end: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-inline-end: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Top
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mt"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-top: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-top: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-top: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Right
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mr"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-right: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-right: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-right: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Margin Bottom
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mb"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-bottom: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-bottom: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-bottom: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion

        #region Margin Left
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ml"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            "margin-left: {value};"
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "margin-left: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "margin-left: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
    
    public static async Task AddSpaceGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Space X
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "space-x"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                margin-left: {value};
            }
            """
            );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            & > * + * {
                margin-left: {value};
            }
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            & > * + * {
                margin-left: {value};
            }
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion

        #region Space Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "space-y"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            & > * + * {
                margin-top: {value};
            }
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            """
            & > * + * {
                margin-top: {value};
            }
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            & > * + * {
                margin-top: {value};
            }
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
}