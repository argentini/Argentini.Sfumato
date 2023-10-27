namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class BorderCollapse : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-collapse";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "border-collapse: collapse;"
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