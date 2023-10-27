namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverflowX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow-x: auto;",
        ["hidden"] = "overflow-x: hidden;",
        ["clip"] = "overflow-x: clip;",
        ["visible"] = "overflow-x: visible;",
        ["scroll"] = "overflow-x: scroll;"
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