namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverscrollY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overscroll-behavior-y: auto;",
        ["contain"] = "overscroll-behavior-y: contain;",
        ["none"] = "overscroll-behavior-y: none;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}