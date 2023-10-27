namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverscrollX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overscroll-behavior-x: auto;",
        ["contain"] = "overscroll-behavior-x: contain;",
        ["none"] = "overscroll-behavior-x: none;",
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