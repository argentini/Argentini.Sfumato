namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Resize : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "resize";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "resize: both;",
        ["none"] = "resize: none;",
        ["y"] = "resize: vertical;",
        ["x"] = "resize: horizontal;"
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