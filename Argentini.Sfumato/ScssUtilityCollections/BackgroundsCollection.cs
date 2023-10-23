using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class BackgroundsCollection
{
    public static async Task AddBackgroundGroupAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bg"
        };

        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("color", "background-color: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "background-size: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("url", "background-image: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "background-position: {value};");

        #endregion
        
        #region Repeat
        
        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["repeat"] = "repeat",
                ["no-repeat"] = "no-repeat"
            },
            "background-repeat: {value};"
        );
        
        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["repeat-x"] = "border-box",
                ["repeat-y"] = "padding-box",
                ["repeat-round"] = "round",
                ["repeat-space"] = "space"
            },
            "background-repeat: {value};"
        );
        
        #endregion
        
        #region Color

        await scssUtilityClass.AddClassAsync(
            SfumatoScss.Colors,
            "background-color: {value};"
        );
            
        #endregion
        
        #region Origin
        
        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["origin-border"] = "border-box",
                ["origin-padding"] = "padding-box",
                ["origin-content"] = "content-box"
            },
            "background-origin: {value};"
        );
        
        #endregion
        
        #region Attachment
        
        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["fixed"] = "fixed",
                ["local"] = "local",
                ["scroll"] = "scroll"
            },
            "background-attachment: {value};"
        );

        #endregion

        #region Clip

        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["border"] = "border-box",
                ["padding"] = "padding-box",
                ["content"] = "content-box",
                ["text"] = "text"
            },
            "background-clip: {value};"
        );

        #endregion
        
        #region Position
        
        await scssUtilityClass.AddClassAsync(
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
            "background-position: {value};"
        );

        #endregion
        
        #region Size
        
        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["auto"] = "auto",
                ["cover"] = "cover",
                ["contain"] = "contain"
            },
            "background-size: {value};"
        );

        #endregion
        
        #region Image

        await scssUtilityClass.AddClassAsync(
            new Dictionary<string,string>
            {
                ["none"] = "none"
            },
            "background-image: {value};"
        );
        
        #endregion
     
        #region Gradient To
        
        await scssUtilityClass.AddClassAsync(
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
            "{value};"
        );

        #endregion
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        await Task.CompletedTask;
    }
    
    public static async Task AddFromGroupAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "from",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        await scssUtilityClass.AddAbitraryValueClassAsync(
            "color",
            $$"""
              --sf-gradient-from: {value} var(--sf-gradient-from-position);
              --sf-gradient-to: transparent var(--sf-gradient-to-position);
              --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
              """
        );

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "--sf-gradient-from-position: {value};");
        
        #endregion
        
        #region Inherit

        await scssUtilityClass.AddClassAsync(new Dictionary<string, string>
        {
            ["inherit"] = """
                          --sf-gradient-from: inherit var(--sf-gradient-from-position);
                          --sf-gradient-to: rgb(255 255 255 / 0) var(--sf-gradient-to-position);
                          --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                          """
        }, "{value}");

        #endregion
        
        #region Color
        
        await scssUtilityClass.AddClassAsync(
            SfumatoScss.Colors,
            $$"""
              --sf-gradient-from: {value} var(--sf-gradient-from-position);
              --sf-gradient-to: transparent var(--sf-gradient-to-position);
              --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
              """
        );
        
        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClass.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClass.SelectorPrefix}-{x}%",
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-from-position: {value};"
            });
        }
        
        #endregion
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddViaGroupAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "via",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        await scssUtilityClass.AddAbitraryValueClassAsync(
            "color",
            $$"""
              --sf-gradient-to: transparent  var(--sf-gradient-to-position);
              --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
              """
        );

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "--sf-gradient-via-position: {value};");
        
        #endregion
        
        #region Inherit
        
        await scssUtilityClass.AddClassAsync(new Dictionary<string, string>
        {
            ["inherit"] = """
                          --sf-gradient-to: rgb(255 255 255 / 0)  var(--sf-gradient-to-position);
                          --sf-gradient-stops: var(--sf-gradient-from), inherit var(--sf-gradient-via-position), var(--sf-gradient-to);
                          """
        }, "{value}");

        #endregion
        
        #region Color
        
        await scssUtilityClass.AddClassAsync(
            SfumatoScss.Colors,
            $$"""
              --sf-gradient-to: transparent  var(--sf-gradient-to-position);
              --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
              """
        );

        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClass.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClass.SelectorPrefix}-{x}%",
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-via-position: {value};"
            });
        }
        
        #endregion
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddToGroupAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "to",
            Category = "gradients"
        };

        #region Arbitrary Value Options
        
        await scssUtilityClass.AddAbitraryValueClassAsync("color", "--sf-gradient-to: {value} var(--sf-gradient-to-position);");
        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "--sf-gradient-to-position: {value};");
        
        #endregion
        
        #region Inherit
        
        await scssUtilityClass.AddClassAsync(new Dictionary<string, string>
        {
            ["inherit"] = "--sf-gradient-to: inherit var(--sf-gradient-to-position);"
                          
        }, "{value}");
        
        #endregion
        
        #region Color

        await scssUtilityClass.AddClassAsync(
            SfumatoScss.Colors,
            "--sf-gradient-to: {value} var(--sf-gradient-to-position);"
        );
        
        #endregion
        
        #region Color Stops From Percentages
        
        for (var x = 0; x <= 100; x+=5)
        {
            scssUtilityClass.Classes.Add(new ScssUtilityClass
            {
                Selector = $"{scssUtilityClass.SelectorPrefix}-{x}%",
                Value = $"{x}%",
                ScssTemplate = "--sf-gradient-to-position: {value};"
            });
        }
        
        #endregion
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
}