namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Overflow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow: auto;",
        ["hidden"] = "overflow: hidden;",
        ["clip"] = "overflow: clip;",
        ["visible"] = "overflow: visible;",
        ["scroll"] = "overflow: scroll;"
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