namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayTableHeaderGroup : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-header-group";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-header-group;"
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