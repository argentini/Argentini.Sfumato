using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class BackgroundsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllBackgroundClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 100000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddBackgroundGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddFromGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddViaGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddToGroupAsync(sortSeed);

        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddBackgroundGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bg"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "background-color: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "background-size: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("url", "background-image: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "background-position: {value};", sortSeed);

        #endregion
        
        #region Repeat
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["repeat"] = "repeat",
                ["no-repeat"] = "no-repeat"
            },
            "background-repeat: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["repeat-x"] = "border-box",
                ["repeat-y"] = "padding-box",
                ["repeat-round"] = "round",
                ["repeat-space"] = "space"
            },
            "background-repeat: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Color

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "background-color: {value};",
            sortSeed
        );
            
        #endregion
        
        #region Origin
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["origin-border"] = "border-box",
                ["origin-padding"] = "padding-box",
                ["origin-content"] = "content-box"
            },
            "background-origin: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Attachment
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["fixed"] = "fixed",
                ["local"] = "local",
                ["scroll"] = "scroll"
            },
            "background-attachment: {value};",
            sortSeed
        );

        #endregion

        #region Clip

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["border"] = "border-box",
                ["padding"] = "padding-box",
                ["content"] = "content-box",
                ["text"] = "text"
            },
            "background-clip: {value};",
            sortSeed
        );

        #endregion
        
        #region Position
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["bottom"] = "bottom",
                ["center"] = "center",
                ["left"] = "left",
                ["left-bottom"] = "left bottom",
                ["left-top"] = "left top",
                ["right"] = "right",
                ["right-bottom"] = "right bottom",
                ["right-top"] = "right top",
                ["top"] = "top"
            },
            "background-position: {value};",
            sortSeed
        );

        #endregion
        
        #region Size
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["auto"] = "auto",
                ["cover"] = "cover",
                ["contain"] = "contain"
            },
            "background-size: {value};",
            sortSeed
        );

        #endregion
        
        #region Image

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none"
            },
            "background-image: {value};",
            sortSeed
        );
        
        #endregion
     
        #region Gradient To
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string,string>
            {
                ["gradient-to-t"] = "background-image: linear-gradient(to top, var(--sf-gradient-stops))",
                ["gradient-to-tr"] = "background-image: linear-gradient(to top right, var(--sf-gradient-stops))",
                ["gradient-to-r"] = "background-image: linear-gradient(to right, var(--sf-gradient-stops))",
                ["gradient-to-br"] = "background-image: linear-gradient(to bottom right, var(--sf-gradient-stops))",
                ["gradient-to-b"] = "background-image: linear-gradient(to bottom, var(--sf-gradient-stops))",
                ["gradient-to-bl"] = "background-image: linear-gradient(to bottom left, var(--sf-gradient-stops))",
                ["gradient-to-l"] = "background-image: linear-gradient(to left, var(--sf-gradient-stops))",
                ["gradient-to-tl"] = "background-image: linear-gradient(to top left, var(--sf-gradient-stops))"
            },
            "{value};",
            sortSeed
        );

        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddFromGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "from",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "color",
            """
            --sf-gradient-from: {value} var(--sf-gradient-from-position);
            --sf-gradient-to: transparent var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
            """,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "--sf-gradient-from-position: {value};", sortSeed);
        
        #endregion
        
        #region Inherit

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["inherit"] = """
                              --sf-gradient-from: inherit var(--sf-gradient-from-position);
                              --sf-gradient-to: rgb(255 255 255 / 0) var(--sf-gradient-to-position);
                              --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                              """
            },
            "{value}",
            sortSeed);

        #endregion
        
        #region Color
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            """
            --sf-gradient-from: {value} var(--sf-gradient-from-position);
            --sf-gradient-to: transparent var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
            """,
            sortSeed
        );
        
        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClassGroup.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClassGroup.SelectorPrefix}-{x}%",
                SortOrder = sortSeed++,
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-from-position: {value};"
            });
        }
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddViaGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "via",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "color",
            """
            --sf-gradient-to: transparent  var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
            """,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "--sf-gradient-via-position: {value};", sortSeed);
        
        #endregion
        
        #region Inherit
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["inherit"] = ""
            },
            """
            --sf-gradient-to: rgb(255 255 255 / 0)  var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), inherit var(--sf-gradient-via-position), var(--sf-gradient-to);
            """,
            sortSeed
            );

        #endregion
        
        #region Color
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            """
            --sf-gradient-to: transparent  var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
            """,
            sortSeed
        );

        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClassGroup.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClassGroup.SelectorPrefix}-{x}%",
                SortOrder = sortSeed++,
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-via-position: {value};"
            });
        }
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddToGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "to",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "--sf-gradient-to: {value} var(--sf-gradient-to-position);", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "--sf-gradient-to-position: {value};", sortSeed);
        
        #endregion
        
        #region Inherit
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["inherit"] = ""
                              
            },
            "--sf-gradient-to: inherit var(--sf-gradient-to-position);",
            sortSeed
        );
        
        #endregion
        
        #region Color

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "--sf-gradient-to: {value} var(--sf-gradient-to-position);",
            sortSeed
        );
        
        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClassGroup.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClassGroup.SelectorPrefix}-{x}%",
                SortOrder = sortSeed++,
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-to-position: {value};"
            });
        }
        
        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
}