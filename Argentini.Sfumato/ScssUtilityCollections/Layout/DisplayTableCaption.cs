namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayTableCaption : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-caption";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-caption;"
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