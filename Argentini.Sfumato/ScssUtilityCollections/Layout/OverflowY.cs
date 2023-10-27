namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverflowY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow-y: auto;",
        ["hidden"] = "overflow-y: hidden;",
        ["clip"] = "overflow-y: clip;",
        ["visible"] = "overflow-y: visible;",
        ["scroll"] = "overflow-y: scroll;"
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