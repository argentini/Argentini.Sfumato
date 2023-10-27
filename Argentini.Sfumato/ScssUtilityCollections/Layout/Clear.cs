namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Clear : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "clear";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["right"] = "clear: right;",
        ["left"] = "clear: left;",
        ["both"] = "clear: both;",
        ["none"] = "clear: none;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}