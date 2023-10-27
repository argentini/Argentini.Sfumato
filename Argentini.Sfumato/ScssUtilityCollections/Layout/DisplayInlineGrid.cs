namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayInlineGrid : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inline-grid";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: inline-grid;"
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