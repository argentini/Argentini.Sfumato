namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Object : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "object";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["contain"] = "object-fit: contain;",
        ["cover"] = "object-fit: cover;",
        ["fill"] = "object-fit: fill;",
        ["none"] = "object-fit: none;",
        ["scale-down"] = "object-fit: scale-down;",
        ["bottom"] = "object-position: bottom;",
        ["center"] = "object-position: center;",
        ["left"] = "object-position: left;",
        ["left-bottom"] = "object-position: left bottom;",
        ["left-top"] = "object-position: left top;",
        ["right"] = "object-position: right;",
        ["right-bottom"] = "object-position: right bottom;",
        ["right-top"] = "object-position: right top;",
        ["top"] = "object-position: top;"
    };

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}