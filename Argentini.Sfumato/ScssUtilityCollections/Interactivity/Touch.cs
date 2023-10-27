namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Touch : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "touch";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "touch-action: auto;",
        ["none"] = "touch-action: none;",
        ["pan-x"] = "touch-action: pan-x;",
        ["pan-left"] = "touch-action: pan-left;",
        ["pan-right"] = "touch-action: pan-right;",
        ["pan-y"] = "touch-action: pan-y;",
        ["pan-up"] = "touch-action: pan-up;",
        ["pan-down"] = "touch-action: pan-down;",
        ["pinch-zoom"] = "touch-action: pinch-zoom;",
        ["manipulation"] = "touch-action: manipulation;"
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